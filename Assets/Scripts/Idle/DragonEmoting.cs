using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEmoting : MonoBehaviour
{
    public Animator Animator;

    void Update ()
    {
        Animator.SetFloat("Happiness", DragonStats.Instance.Happiness);
    }
}
