using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    IAttackStrategy strategy;
    public void SetAttackStrategy(IAttackStrategy strategy)
    {
        this.strategy = strategy;
    }

    // Update is called once per frame
    void Update()
    {
        strategy?.Attack();
    }
}
