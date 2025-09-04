using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTU_PhysicsOverLapCircle : MonoBehaviour
{
    //1 Physics2D.OverlapCircle
    /*
        Chức năng: Phát hiện tất cả các Collider 2D nằm trong một khu vực hình tròn.
        Cú pháp: Physics2D.OverlapCircle(Vector2 point, float radius, int layerMask)
            point (Vector2): Vị trí trung tâm của hình tròn.
            radius (float): Bán kính của hình tròn.
            layerMask (int): Lớp (Layer) mà bạn muốn tìm kiếm.
    */

    //2 Physics2D.OverlapBox
    /*
        Chức năng: Phát hiện tất cả các Collider 2D nằm trong một khu vực hình hộp (hình chữ nhật).
        Cú pháp: Physics2D.OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask)
            point (Vector2): Vị trí trung tâm của hình hộp.
            size (Vector2): Chiều rộng và chiều cao của hình hộp.
            angle (float): Góc xoay của hình hộp (thường để 0).
            layerMask (int): Lớp (Layer) mà bạn muốn tìm kiếm.
    */


    public LayerMask LayerMask;
    public float Radius =5;
    private void Update()
    {
        Vector2 point = transform.position;
        Collider2D[] hit = Physics2D.OverlapCircleAll(point, Radius, LayerMask);
        //Collider2D hitaEnemy = Physics2D.OverlapCircle(point, Radius, LayerMask); nếu mình làm như này thì chỉ tìm được 1 enemy thôi nếu có 2 enemy trong tầm của radius
        foreach (Collider2D enemy in hit)
        {
            Debug.Log("Tìm thấy kẻ thù: " + enemy.name);
        }

    }
}
