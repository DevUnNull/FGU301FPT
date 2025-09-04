using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet
{
    public Transform ShootPoint;
    public Rigidbody2D Rigidbody;
    public float recoilForce = 5f;
    // Update is called once per frame
    public ShootBullet(Transform shootPoint , Rigidbody2D rigidbody, float recoilForce)
    { 
        this.ShootPoint = shootPoint;
        this.Rigidbody = rigidbody;
        this.recoilForce = recoilForce;
    }
    public void Shoot() 
    { 
        GameObject bullet = HTU_ObjectPoolPattern.Instance.GetObject();
        bullet.transform.position = ShootPoint.position;
        // tính hướng giật lùi 
        Vector2 recoilDirection  = (Rigidbody.transform.position - ShootPoint.position).normalized; // a - b luôn bằng b - a vecter

        if (Rigidbody != null) 
        {
            Rigidbody.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse); // ForceMode2D.Impulse: Đây là tham số thứ hai, đại diện cho chế độ lực. 
        }
    }

}
