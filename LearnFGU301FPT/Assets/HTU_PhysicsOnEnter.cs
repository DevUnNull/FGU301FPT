using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;

public class HTU_PhysicsOnEnter : MonoBehaviour
{
    /*
        collision.gameObject: Đối tượng mà bạn vừa va chạm vào.
        collision.transform: Transform của đối tượng đó.
        collision.collider: Collider của đối tượng đó.
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {   // 1 chỉ lấy name của GameObject enemy
            Debug.Log("name GameObjectEnemy : "+collision.collider.name); 
            // 2 lấy ra loại collider của GameObject enemy
            Debug.Log("lấy ra loại collider của GameObject enemy : " + collision.GetType()); 
            // 3 lấy vị trí hiện tại của GameObject enemy
            Debug.Log("lấy vị trí hiện tại của GameObject enemy: " + collision.transform.position);

            // 4. Danh sách điểm tiếp xúc
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Điểm tiếp xúc: " + contact.point);
                Debug.Log("Normal tại điểm tiếp xúc: " + contact.normal);
            }
            // 5. Vận tốc tương đối giữa 2 vật thể lúc va chạm
            Debug.Log("Vận tốc tương đối: " + collision.relativeVelocity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Tên đối tượng đi vào trigger
        Debug.Log("Trigger với: " + collision.gameObject.name);

        // 2. Vị trí hiện tại của đối tượng kia
        Debug.Log("Vị trí đối tượng: " + collision.transform.position);

        // 3. Collider type
        Debug.Log("Loại Collider: " + collision.GetType().Name);

        // 4. Offset giữa collider và transform (nếu có)
        Debug.Log("Offset: " + collision.offset);

        // 5. Kích thước vùng collider (bounds) thường dùng để hiện tầm đánh của player 
        Debug.Log("Bounds center: " + collision.bounds.center);
        Debug.Log("Bounds size: " + collision.bounds.size);

        // 6. Kiểm tra có Rigidbody2D không
        if (collision.attachedRigidbody != null)
        {
            Debug.Log("Đối tượng có Rigidbody2D, khối lượng: " + collision.attachedRigidbody.mass);
        }
        else
        {
            Debug.Log("Đối tượng không có Rigidbody2D.");
        }

        // 7. Đối tượng có phải là trigger không?
        Debug.Log("Đối tượng kia có phải trigger? " + collision.isTrigger);
    }
}
