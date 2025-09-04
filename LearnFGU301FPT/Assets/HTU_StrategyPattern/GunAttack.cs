using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttack : IAttackStrategy
{
    public void Attack()
    {
        Debug.Log("GunAttack");
    }
}
