using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTU_SinglertenPattern : MonoBehaviour
{
    // 1 singleton pattern
    public static HTU_SinglertenPattern Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 2 Object Pool Pattern
}
