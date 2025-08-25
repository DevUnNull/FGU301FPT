using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attribute : MonoBehaviour
{
    [SerializeField]    // cho phep hien trong inspector du la private
    private int heal = 100;

    [HideInInspector]   // Lưu giá trị, nhưng không hiện trong Inspector (nó giống như private nhưng thay vì dùng private mình chỉ cùng ở trong class này thì dùng HideInInspector mình có thể dùng ở các class khác)
    public int damage = 10;

    [NonSerialized]
    public int tempValue = 0;  // Không lưu, không hiện trong Inspector (có nghĩa là dù nó trong game có thay đổi như nào thì lúc out scene hoặc thoát game thì nó cũng sẽ quay lại giá trị ban đầu)


    private void Update()
    {
        // test NonSerialized
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempValue++;
            Debug.Log("heal=" + heal + " combo=" + tempValue);
        }
    }
}

/*[Serializable]   // Cho phép Unity serialize class này
public class Weapon
{
    public string name;
    public int damage;
}
//  có nghĩa lúc này ở trên inspector có scrip Player thì trên inspector cũng hiện dữ liệu của name và damage để mình chỉnh sửa

public class Player : MonoBehaviour
{
    public Weapon weapon; // Unity giờ có thể serialize Weapon
}*/

