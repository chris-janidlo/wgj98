using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerDragon : MonoBehaviour
{
    public float MoveSpeed, DropSpeed, JumpDelay, MaxAnimationSpeed, DragonSpeedForMaxAnimationSpeed;
    [Range(0, 360)]
    public float TurnSpeed;
    [Range(0, 1)]
    public float ContactThreshold, GroundedThreshold, CriticalVerticality;
    public AnimationCurve JumpCurve;

    float jumpTime => JumpCurve.keys[JumpCurve.keys.Length - 1].time;
    bool jumping => jumpTimer <= jumpTime;

    Rigidbody2D rb;
    float jumpTimer, jumpDelayTimer;
    bool touchingGround;

    GameObject body;
    SpriteRenderer bodySprite;
    Animator bodyAnimator;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = Vector3.zero; // so we rotate around the head instead of the body
        jumpTimer = jumpTime;

        body = transform.GetChild(0).gameObject;
        bodySprite = body.GetComponent<SpriteRenderer>();
        bodyAnimator = body.GetComponent<Animator>();
    }

    void Update ()
    {
        jumpDelayTimer -= Time.deltaTime;
        jumpTimer += Time.deltaTime;

        if (jumpDelayTimer <= 0 && Input.GetButton ("Jump"))
        {
            jumpDelayTimer = JumpDelay;
            jumpTimer = 0;

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

            bodyAnimator.Play("DragonPlatformer_flap", 0, 0);
        }
        
        if (jumping)
        {
            // flap animation is timed precisely, want to preserve that
            bodyAnimator.speed = 1;
        }
        else
        {
            var animationSpeedLerpent = Mathf.Clamp(rb.velocity.magnitude, 0, DragonSpeedForMaxAnimationSpeed) / DragonSpeedForMaxAnimationSpeed;
            bodyAnimator.speed = Mathf.Lerp(1, MaxAnimationSpeed, animationSpeedLerpent);
        }
    }

    void FixedUpdate ()
    {
        // rb.rotation doesn't constrain itself to [0, 360), while transform.rotation.eulerAngles.z lags behind by a frame
        var effectiveRotation = Mathf.Repeat(rb.rotation, 360);

        var vert = verticality(effectiveRotation);

        bool grounded = touchingGround && vert <= GroundedThreshold;

        if (grounded)
        {
            rb.MovePosition(rb.position + Vector2.right * MoveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        }
        else
        {
            if (!jumping)
            {
                var rotateDelta = TurnSpeed * -Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                rb.MoveRotation(rb.rotation + rotateDelta);
            }
            bodySprite.flipY = effectiveRotation > 90 && effectiveRotation < 270;
        }

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

        // simulate stalling by letting the rigidbody fall however it wants to if its vertical enough
        if (vert >= CriticalVerticality)
        {
            rotDir.y = Mathf.Sign(rb.velocity.y);
        }

        // simulate glider drag by redistributing the velocity among the x and y axes depending on how vertical we are
        var newVel = new Vector2
        (
            rotDir.x * (1 - vert),
            Input.GetButton("Drop") ? DropSpeed : rotDir.y * vert
        ).normalized * rb.velocity.magnitude;

        rb.velocity = newVel;

        if (jumping)
        {
            rb.MovePosition(rb.position + Vector2.up * JumpCurve.Evaluate(jumpTimer));
        }
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        foreach (var contact in col.contacts)
        {
            if (contact.normal.y >= ContactThreshold)
            {
                touchingGround = true;
            }
        }
    }

    void OnCollisionExit2D (Collision2D col)
    {
        touchingGround = false;
    }

    // returns 0 if we're perfectly horizontal, 1 if perfectly vertical, or something in between
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
