using System.Collections;
using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public float speed = 10;
    public Vector2 right = Vector2.right;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") ) 
        { 
            Deactivete();
        }
    }
    private void OnEnable() // được gọi ngay sau khi gameObject được SetActive(true)
    {
        // Gọi một lần duy nhất khi bullet được lấy ra từ pool
        Invoke(nameof(Deactivete), 3f);
    }

    private void OnDisable()// được gọi ngay sau khi gameObject được SetActive(false)
    {
        // Hủy mọi Invoke nếu object bị disable sớm tránh tình trạng còn thừa vài giây cái sau gọi tiếp
        CancelInvoke();
    }

    private void Update()
    {
        transform.Translate(right * speed * Time.deltaTime);
    }

    void Deactivete()
    {
        HTU_ObjectPoolPattern.Instance.ReturnObject(this.gameObject);
    }
}
