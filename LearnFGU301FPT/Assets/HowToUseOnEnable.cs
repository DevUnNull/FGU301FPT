using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UIElements.VisualElement;

public class HowToUseOnEnable : MonoBehaviour
{
    /* - Khi nào được gọi?
                +Sau Awake() và trước Start(), ở lần bật đầu tiên.
                +Mỗi lần bạn gọi gameObject.SetActive(true) hoặc myComponent.enabled = true (và object đang active trong hierarchy).
                +Không gọi nếu GameObject đang inactive ở scene load(tới khi bạn bật lại).
                +Với[ExecuteInEditMode]/[ExecuteAlways], có thể được gọi cả trong Editor khi reload script, mở scene, v.v.
       -So sánh nhanh
                +Awake(): gọi một lần khi object được nạp (dù có active hay không). Dùng để cache reference.
                +OnEnable(): gọi mỗi lần bật. Dùng để đăng ký sự kiện / khởi động state tạm thời.
                +Start(): gọi một lần trước frame update đầu tiên, chỉ khi object đang enabled & active.
                +OnDisable(): cặp với OnEnable(); gọi khi tắt để hủy đăng ký / dọn dẹp.
        OnDestroy(): gọi khi object bị hủy.
     */
    
    [SerializeField] GameObject player;
    [SerializeField] HowToUseOnEnable myScript; // có thể là chính script này hoặc script khác
    private void OnEnable()
    {
        if (player) player.SetActive(true);
        Debug.Log("👉 OnEnable chạy!");
    }

    // sẽ được gọi khi enabled của scrip == false
    private void OnDisable()
    {
        // Chỉ đụng vào player nếu nó còn tồn tại
        if (player && player.activeSelf) //activeSelf là trạng thái hiện tại của player (và ở điều kiện này nó phải true thì mới được)
            player.SetActive(false);

        Debug.Log("❌ OnDisable chạy!");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ✅ CHUẨN: toggle trạng thái script để Unity tự gọi OnEnable/OnDisable
            if (myScript) myScript.enabled = !myScript.enabled;
            // Nếu muốn ẩn/hiện player thì:
            if (player) player.SetActive(!player.activeSelf);
            Debug.Log("da an nut");
        }
    }

    public void testGetComponent() 
    {
        Debug.Log("testGetComponent thanh cong");    
    }

}
