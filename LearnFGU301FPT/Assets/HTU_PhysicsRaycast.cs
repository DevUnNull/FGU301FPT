using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class HTU_PhysicsRaycast : MonoBehaviour
{
    // Khoảng cách từ chân nhân vật để bắn tia xuống
    public float raycastDistance = 0.5f;

    // Lớp của mặt đất
    public LayerMask groundLayer; // nó sẽ lấy ở phần Layer mà mình đặt

    void Update()
    {
        // Vị trí của tia
        Vector2 origin = transform.position;

        // Hướng bắn
        Vector2 direction = Vector2.down;

        // Bắn tia Raycast xuống
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, raycastDistance, groundLayer);
        // RaycastHit2D hit Raycast(vị trí tia , hướng tia , độ dài tia , vị trí phát hiện colider)

        // Kiểm tra xem tia có va chạm với vật thể nào không
        // hit.collider : sẽ trả về null hoặc trả về tên của GameObject mà tia hit chạm vào + thêm chữ collider
        // hit.rigidbody : sẽ trả về null hoặc trả về tên của GameObject mà tia hit chạm vào + thêm chữ Rigidbody
        // hit.point : sẽ trả về 0,0 hoặc vị trí mà tia đang chỉ đến 
        if (hit.collider != null)
        {
            Debug.Log("Nhân vật đang đứng trên mặt đất." + hit.point);
        }
        else
        {
            Debug.Log("Nhân vật đang ở trên không nhé." + hit.point);
        }
    }

    // Vẽ tia Raycast trong Scene view để dễ hình dung
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * raycastDistance);
    }
}
