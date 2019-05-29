using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGem : MonoBehaviour
{
    public float Value = 1;
    
    void OnTriggerEnter2D ()
    {
        Bank.Instance.IncrementMoney(Value);
        Destroy(gameObject);
    }
}
