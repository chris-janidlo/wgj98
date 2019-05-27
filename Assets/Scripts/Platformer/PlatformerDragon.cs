using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerDragon : MonoBehaviour
{
    public float TurnSpeed, CriticalVerticality;

    Rigidbody2D rb;
    bool flapped;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        if (!flapped)
        {
            flapped = Input.GetButtonDown("Jump");
        }
    }

    void FixedUpdate ()
    {
        var rotateDelta = TurnSpeed * -Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        rb.MoveRotation(rb.rotation + rotateDelta);

        // rb.rotation doesn't constrain itself to [0, 360), while transform.rotation.eulerAngles.z lags behind by a frame
        var effectiveRotation = Mathf.Repeat(rb.rotation, 360);

        Vector2 rotDir;
        switch ((int) (effectiveRotation / 90))
        {
            case 4:
            case 0:
                rotDir = new Vector2(1, 1);
                break;
            case 1:
                rotDir = new Vector2(-1, 1);
                break;
            case 2:
                rotDir = new Vector2(-1, -1);
                break;
            case 3:
                rotDir = new Vector2(1, -1);
                break;
            default:
                throw new System.Exception($"unexpected rotation value {effectiveRotation}");
        }

        var vert = verticality(effectiveRotation);

        if (vert >= CriticalVerticality)
        {
            rotDir.y = Mathf.Sign(rb.velocity.y);
        }

        var newVel = new Vector2
        (
            rotDir.x * (1 - vert),
            rotDir.y * vert
        ).normalized * rb.velocity.magnitude;

        rb.velocity = newVel;
    }

    // returns 0 if perfectly horizontal, 1 if perfectly vertical, or something in between
    float verticality (float angleDegrees)
    {
        // triangle wave with period of 90, min of 0, and max of 1

        float pos = Mathf.Repeat(angleDegrees, 180) / 180;

        if (pos < .5f)
        {
            return Mathf.Lerp(0, 1, pos * 2);
        }
        else
        {
            return Mathf.Lerp(1, 0, (pos - .5f) * 2);
        }
    }
}
