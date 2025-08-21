using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToUseGetComponent : MonoBehaviour
{
    /*
     - GetComponent là gì?
	    Đây là hàm của GameObject để lấy component (thành phần) đang gắn trên chính object đó.
	    trả về component bạn yêu cầu (ví dụ: Rigidbody, Collider, Renderer, hoặc script bạn tự viết).
     - câu lệnh cơ bản 
	    T component = gameObject.GetComponent<T>();

    hiểu kỹ hơn tại HowToUseGetComponent tại LearnFGU301FPT
     */
    //1 lấy component bình thường
    private Rigidbody2D rb; // khởi tạo 
    void Awake()
    {
        // Lấy Rigidbody gắn trên chính object này
        rb = GetComponent<Rigidbody2D>(); // hiểu nôm na là gán rb = với Rigidbody mà Object đang sở hữu ở inspector
    }
    void Update()
    {
        // Thêm lực khi nhấn U
        if (Input.GetKeyDown(KeyCode.U))
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }

    //2 lấy component scrip
    HowToUseOnEnable HowToUseOnEnable;
    private void Start()
    {
        HowToUseOnEnable = GetComponent<HowToUseOnEnable>(); // dùng kiểu này hoặc cũng có thể dùng bằng  [SerializeField] để kéo scrip HowToUseOnEnable vào
        HowToUseOnEnable.testGetComponent();
    }

    //3 tôi nghĩ là ít dùng Lấy component ở object con hoặc cha 
    /*
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();
        Rigidbody parentRb = GetComponentInParent<Rigidbody>();
     */
}
