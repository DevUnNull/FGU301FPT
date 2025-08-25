using UnityEngine;

public class TouchDemo : MonoBehaviour
{
    [Header("Swipe")]
    public float swipeMinDistance = 80f;

    [Header("Pinch")]
    public Camera cam;
    public float zoomSpeed = 0.01f; // chỉnh cho vừa tay
    public float minFov = 25f, maxFov = 80f; // dùng cho camera perspective
    public float minOrtho = 2f, maxOrtho = 20f; // dùng cho camera orthographic

    Vector2 swipeStart;
    bool swiping;

    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void Update()
    {
        // Fallback: test trong Editor bằng chuột
        if (!Input.touchSupported)
        {
            if (Input.GetMouseButtonDown(0)) OnTapOrDragStart(Input.mousePosition);
            if (Input.GetMouseButton(0)) OnDrag(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) OnTapOrDragEnd(Input.mousePosition);
            return;
        }

        // --- 1 ngón: Tap / Drag / Swipe ---
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            switch (t.phase)
            {
                case TouchPhase.Began:
                    OnTapOrDragStart(t.position);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    OnDrag(t.position);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    OnTapOrDragEnd(t.position);
                    break;
            }
        }

        // --- 2 ngón: Pinch Zoom ---
        if (Input.touchCount >= 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float prevDist = (t0.position - t0.deltaPosition - (t1.position - t1.deltaPosition)).magnitude;
            float currDist = (t0.position - t1.position).magnitude;
            float delta = currDist - prevDist; // >0 nở ra, <0 khép vào

            if (cam != null)
            {
                if (cam.orthographic)
                {
                    cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - delta * zoomSpeed, minOrtho, maxOrtho);
                }
                else
                {
                    cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - delta * zoomSpeed, minFov, maxFov);
                }
            }
        }
    }

    void OnTapOrDragStart(Vector2 screenPos)
    {
        swiping = true;
        swipeStart = screenPos;

        // Raycast xem có chạm vào vật thể không
        Ray ray = cam.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // ví dụ: chọn object
            Debug.Log("Tap bắt đầu trên: " + hit.collider.name);
        }
        // 2D: dùng Physics2D.GetRayIntersection(ray, out RaycastHit2D hit2D)
    }

    void OnDrag(Vector2 screenPos)
    {
        // ví dụ: kéo object theo ngón tay nếu muốn
        // (tự giữ tham chiếu tới object đã "bắt" ở OnTapOrDragStart)
    }

    void OnTapOrDragEnd(Vector2 screenPos)
    {
        if (!swiping) return;
        swiping = false;

        // Tap hay Swipe?
        float dist = Vector2.Distance(swipeStart, screenPos);
        if (dist < swipeMinDistance)
        {
            Debug.Log("Tap!");
        }
        else
        {
            Vector2 dir = (screenPos - swipeStart).normalized;
            string swipeDir =
                Mathf.Abs(dir.x) > Mathf.Abs(dir.y)
                ? (dir.x > 0 ? "Swipe Right" : "Swipe Left")
                : (dir.y > 0 ? "Swipe Up" : "Swipe Down");
            Debug.Log(swipeDir);
        }
    }
}
