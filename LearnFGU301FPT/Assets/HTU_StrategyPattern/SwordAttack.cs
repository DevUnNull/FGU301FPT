using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : IAttackStrategy
{
    public void Attack()
    {
        Debug.Log("Sword Attack");
    }
}
