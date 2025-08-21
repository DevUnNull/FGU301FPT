using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerSwipeMove2D : MonoBehaviour
{
    [Header("Movement (Speed-based)")]
    public float moveSpeed = 6f;          // tốc độ mục tiêu (đơn vị / giây)
    public float lerpFactor = 6f;         // (giữ lại để bạn vi chỉnh nếu muốn)

    [Header("Swipe")]
    public float swipeMinDistance = 100f; // px – tối thiểu để tính là swipe vuốt

    [Header("Scale (Zoom)")]
    public float pinchScaleFactor = 0.005f; //Tốc độ scale khi pinch: delta khoảng cách hai ngón * hệ số này.
    public float clickScaleStep = 0.15f; //Bước scale cho double/triple click (PC test).
    public float minScale = 0.4f; //Giới hạn scale.
    public float maxScale = 3f; //Giới hạn scale.

    [Header("Mouse (PC Test)")]
    public float multiClickThreshold = 0.35f; //Khoảng thời gian gom click để nhận biết double/triple click.
    public float dragAsSwipeThreshold = 25f; //Nếu kéo xa hơn ngưỡng này khi giữ chuột, coi là drag/swipe, huỷ logic multi-click.

    [Header("Collision/Sweep")]
    public float skin = 0.01f;            // khoảng an toàn để không chèn sát quá
    public ContactFilter2D contactFilter; // cấu hình layer, trigger, normalAngle…

    private Rigidbody2D rb;
    private Collider2D col;
    private Camera cam;

    // hướng mục tiêu sau khi vuốt
    private Vector2 targetDir = Vector2.down;

    // swipe state
    private Vector2 swipeStart;
    private bool swiping;

    // double / triple click state
    private int clickCount = 0;
    private float lastClickTime = -10f;
    private Coroutine clickCo;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        cam = Camera.main;

        // Vật lý 2D an toàn hơn cho vật thể di chuyển nhanh:
        rb.gravityScale = 0f; //để player không thể bị rớt nữa
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // hoặc ContinuousSpeculative / Bật CCD 2D để giảm nguy cơ xuyên khi tốc độ cao. Có thể thử
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;         // mượt render, không ảnh hưởng va chạm

        // Tuỳ nhu cầu: bỏ qua trigger, chỉ va chạm layer mong muốn
        // contactFilter.useTriggers = false; // mặc định false
        // contactFilter.SetLayerMask(LayerMask.GetMask("Default", "Wall", "Obstacle"));
        // contactFilter.useLayerMask = true;
    }

    void Update() //Ưu tiên input touch nếu thiết bị hỗ trợ và đang chạm; ngược lại dùng chuột (test PC).
    {
        if (Input.touchSupported && Input.touchCount > 0)
            HandleTouch();
        else
            HandleMouse(); // test PC
    }

    void FixedUpdate() //trái tim chống xuyên
    {
        // ---- THAY VÌ ĐẨY BẰNG VELOCITY, TA "QUÉT RỒI DÍCH" (CAST + MovePosition) ----

        // Hướng mục tiêu (đã gán bởi swipe)
        Vector2 dir = targetDir.sqrMagnitude > 0f ? targetDir.normalized : Vector2.zero;
        if (dir == Vector2.zero) return;

        // Quãng muốn đi trong frame vật lý
        float maxMove = moveSpeed * Time.fixedDeltaTime;

        // Quét theo collider hiện tại (ổn hơn raycast điểm)
        RaycastHit2D[] hits = new RaycastHit2D[4];
        int hitCount = rb.Cast(dir, contactFilter, hits, maxMove);

        float moveDist = maxMove;

        if (hitCount > 0)
        {
            // Lấy va chạm gần nhất theo khoảng cách
            float nearest = float.MaxValue;
            for (int i = 0; i < hitCount; i++)
            {
                // Bỏ qua va chạm quá sát/âm do số học
                if (hits[i].distance >= 0f && hits[i].distance < nearest)
                    nearest = hits[i].distance;
            }

            // Dừng ngay trước điểm chạm, chừa "skin"
            moveDist = Mathf.Max(0f, nearest - skin);
        }

        // Di chuyển an toàn theo khoảng còn lại
        if (moveDist > 0f)
            rb.MovePosition(rb.position + dir * moveDist);
        else
        {
            // Đang dính tường: có thể gán nhẹ velocity=0 để tránh tích luỹ
            rb.velocity = Vector2.zero;
        }
    }

    // ==================== TOUCH ====================
    void HandleTouch()
    {
        // 1 ngón: vuốt để đổi hướng di chuyển (targetDir)
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    swiping = true;
                    swipeStart = t.position;
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (!swiping) break;
                    swiping = false;
                    DetectSwipeAndSetDir(swipeStart, t.position);
                    break;
            }
        }

        // 2 ngón: pinch để scale player (xem lưu ý ở cuối)
        if (Input.touchCount >= 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currDist = (t0.position - t1.position).magnitude;
            float delta = currDist - prevDist;

            ApplyScaleDelta(delta * pinchScaleFactor);
        }
    }

    // ==================== MOUSE (PC) ====================
    void HandleMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime <= multiClickThreshold) clickCount++;
            else clickCount = 1;

            lastClickTime = Time.time;

            if (clickCo != null) StopCoroutine(clickCo);
            clickCo = StartCoroutine(ResolveClicksAfterDelay());

            swiping = true;
            swipeStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (swiping && Vector2.Distance(swipeStart, (Vector2)Input.mousePosition) > dragAsSwipeThreshold)
            {
                if (clickCo != null) StopCoroutine(clickCo);
                clickCount = 0;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 end = Input.mousePosition;
            float dist = Vector2.Distance(swipeStart, end);

            if (dist >= swipeMinDistance)
            {
                if (clickCo != null) StopCoroutine(clickCo);
                clickCount = 0;
                DetectSwipeAndSetDir(swipeStart, end);
            }
        }
    }

    IEnumerator ResolveClicksAfterDelay()
    {
        yield return new WaitForSeconds(multiClickThreshold);

        if (clickCount == 2) ApplyScaleDelta(+clickScaleStep); // double: zoom in
        else if (clickCount >= 3) ApplyScaleDelta(-clickScaleStep); // triple: zoom out

        clickCount = 0;
        clickCo = null;
    }

    // ==================== COMMON ====================
    void DetectSwipeAndSetDir(Vector2 start, Vector2 end)
    {
        Vector2 diff = end - start;
        if (diff.magnitude < swipeMinDistance) return;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
            targetDir = diff.x > 0 ? Vector2.right : Vector2.left;
        else
            targetDir = diff.y > 0 ? Vector2.up : Vector2.down;

        Debug.Log("TargetDir = " + targetDir);
    }

    void ApplyScaleDelta(float delta)
    {
        // ⚠️ Khuyến nghị: KHÔNG scale trực tiếp object có Rigidbody2D/Collider2D.
        // Tốt nhất: để Sprite là con (child) và scale child, giữ thân vật lý kích thước cố định.
        float current = transform.localScale.x;
        float target = Mathf.Clamp(current + delta, minScale, maxScale);
        transform.localScale = new Vector3(target, target, transform.localScale.z);
        Debug.Log("Scale = " + target.ToString("F2"));
    }
}
