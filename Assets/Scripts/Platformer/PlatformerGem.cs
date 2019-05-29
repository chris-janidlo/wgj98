using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformerGem : MonoBehaviour
{
    public UnityEvent Collected;
    public float Value = 1;
    
    void OnTriggerEnter2D ()
    {
        Bank.Instance.IncrementMoney(Value);
        Destroy(gameObject);
        Collected.Invoke();
    }
}
