using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTU_ObjectPoolPattern : MonoBehaviour
{
    public static HTU_ObjectPoolPattern Instance { get; private set; }

    public GameObject objectPrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();
    void Awake()
    {
        // Kiểm tra nếu đã có thể hiện nào tồn tại
        if (Instance != null && Instance != this)
        {
            // Nếu có, hủy đối tượng hiện tại để đảm bảo tính duy nhất
            Destroy(this.gameObject);
        }
        else
        {
            // Nếu chưa, gán thể hiện hiện tại vào biến Instance
            Instance = this;
            InitializePool();
        }

    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Nếu hết object trong pool, tạo thêm (nên hạn chế)
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}


//------------
