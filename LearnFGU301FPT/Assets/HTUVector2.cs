using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HTUVector2 : MonoBehaviour
{
    
    [SerializeField]
    public Transform target;
    public void main()
    {
        //1.Khởi tạo và truy cập Vector2
        Vector2 pos = new Vector2(3f, 5f);
        Debug.Log(pos.x); // 3
        Debug.Log(pos.y); // 5

        //2.Phép toán cơ bản
        Vector2 a = new Vector2(2, 3);
        Vector2 b = new Vector2(1, 1);
        Vector2 sum = a + b;    // (3,4)
        Vector2 diff = a - b;   // (1,2)
        Vector2 scaled = a * 2; // (4,6)
        float dot = Vector2.Dot(a, b); // 2*1 + 3*1 = 5

        //3. Khoảng cách và độ dài
        Vector2 a1 = new Vector2(0, 0);
        Vector2 b1 = new Vector2(3, 4);
        float distance = Vector2.Distance(a1, b1); // 5
        float magnitude = a1.magnitude; // độ dài vector a
        Vector2 normalized = b1.normalized; // (0.6, 0.8)

        //4. Di chuyển và nội suy
        Vector2 start = new Vector2(0, 0);
        Vector2 end = new Vector2(10, 0);
        // Di chuyển dần dần từ start về end
        Vector2 move = Vector2.MoveTowards(start, end, 0.1f); //(nó khá tương tự với lerp)
        // nhưng ở hàm này khác là mỗi lần gọi thì nó sẽ dịch chuyển 0,1 , nếu như khoảng cách giữa start và end < 0.1 thì nó sẽ trả về end , có nghĩa là đứng ngay tại end luôn
        // Nội suy tuyến tính (nội suy là khái niệm toán học của lập trình , nó dùng để ước lượng giá trị nằm giữa 2 hoặc nhiều giá trị đã biết)
        Vector2 lerp = Vector2.Lerp(start, end, 0.5f); // (5,0)
        /*
         Vector2.Lerp(Vector2 a, Vector2 b, float t);
            Cách hoạt động
                a: vector bắt đầu.
                b: vector kết thúc.
                t: giá trị từ 0 → 1, đại diện cho phần trăm tiến trình.
                    t = 0 → kết quả là a
                    t = 1 → kết quả là b
                    0 < t < 1 → kết quả nằm giữa a và b.
         */
        //Ứng dụng trong Unity
        //Di chuyển đối tượng đuổi theo target
        transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime);





        //5. Góc và hướng
        Vector2 dir = new Vector2(1, 1).normalized; // nó sẽ trả về 1 vector cùng hướng với (1,1) nhưng độ dài bằng 1 (nếu đúng ra thì dir = 1.414 nhưng có .normalized thì dir = 1 và có tọa độ là (1/'căng2',1/'căn 2'))
        // nói chung tác dụng của nó là dữ nguyên hướng đi của vector chỉ bỏ giá trị của vector trả về 1 thôi , để tránh tình trạng tốc độ chạy không đồng đều trên toàn bộ thời gian game
        float angle = Vector2.Angle(Vector2.right, dir); // 45 độ
        //👉 Tip: Để quay một vector theo góc:
        float rad = 45 * Mathf.Deg2Rad;
        Vector2 rotated = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

    }

    // Ứng dụng điển hình trong unity di chuyển player 2d
    private Rigidbody2D rb;
    public float speed = 2;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        /*
            Nhấn W → (0,1)
            Nhấn D → (1,0)
            Nhấn W + D → (1,1) vậy nên lúc này normalized sẽ có tác dụng là lúc đi chéo thì tốc độ vẫn bằng 1 chứ không phải là 1.4
            
            Nếu KHÔNG .normalized
            Khi bạn nhấn W hoặc D (1 trục) → vector độ dài = 1 → tốc độ = speed.
            Khi bạn nhấn W + D (chéo, 2 trục) → vector (1,1) có độ dài = √2 ≈ 1.414
            → nhân vật sẽ đi nhanh hơn 1.414 lần so với đi thẳng.
         */
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * speed;
    }

    public void Update()
    {
        main();
    }


}
