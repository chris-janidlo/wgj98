using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformerGem : MonoBehaviour
{
    public UnityEvent Collected;
    public AudioClip CollectedClip;
    public float Value = 1;
    
    void OnTriggerEnter2D ()
    {
        Bank.Instance.IncrementMoney(Value);
        SFX.Play(CollectedClip);
        Destroy(gameObject);
        Collected.Invoke();
    }
}
