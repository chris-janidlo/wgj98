using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float VertPeriod, VertAmplitude, RotPeriod, RotAmplitude;

    float startY;

    void Start ()
    {
        startY = transform.position.y;    
    }

    void Update ()
    {
        var newY = VertAmplitude * Mathf.Sin((2 * Mathf.PI / VertPeriod) * Time.time) + startY;
        transform.position = new Vector2(transform.position.x, newY);

        var newRot = RotAmplitude * Mathf.Sin((2 * Mathf.PI / RotPeriod) * Time.time);
        transform.rotation = Quaternion.Euler(Vector3.forward * newRot);
    }
}
