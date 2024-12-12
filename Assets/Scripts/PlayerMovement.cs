using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform headTransform;
    [SerializeField] Transform footTransform;
    [SerializeField] Transform gfxTransform;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask deadZoneLayer;
    [SerializeField] LayerMask boxLayer;

    [SerializeField] float speed = 10.0f;
    [SerializeField] float jumpSpeed = 2.0f;
    [SerializeField] float gravity = -20.0f;
    [SerializeField] float jumpTimerSet = 0.2f;
    [SerializeField] float knockbacktimerSet = 0.1f;

    float knockbacktimer;
    float jumpTimer;
    bool isDead = false;
    float velocityXSmoothing;
    Vector2 targetVelocity;
    Rigidbody2D rb;
    private float fallTimer;
    RaycastHit2D[] hits = new RaycastHit2D[10];


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, gravity);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (isDead)
        {
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        float targetVelocityX = horizontalInput * speed;
        float targetVelocityY = 0;


        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = jumpTimerSet;
        }

        jumpTimer -= Time.deltaTime;

        if (jumpTimer > 0 && knockbacktimer <= 0)
        {
            targetVelocityY = jumpSpeed;
        }
        else
        {
            targetVelocityY = rb.velocity.y;
        }

        if (rb.velocity.x > 0.1f)
        {
            gfxTransform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < -0.1f)
        {
            gfxTransform.localScale = new Vector3(-1, 1, 1);
        }


        if (knockbacktimer > 0)
        {
            knockbacktimer -= Time.deltaTime;
        }

        targetVelocity = new Vector2(targetVelocityX, targetVelocityY);


        rb.velocity = targetVelocity;

        if (IsTouchingDeadZone())
        {
            isDead = true;
            Invoke(nameof(Restart), 2f);
        }
    }

    private void FixedUpdate()
    {
        CheckForBoxCollision();
    }

    private void CheckForBoxCollision()
    {
        if (rb.velocity.y > 0.2f)
        {
            int hitCount = Physics2D.BoxCastNonAlloc(headTransform.position,new Vector2(0.7f,0.5f),0,Vector2.up,hits,0.1f,boxLayer);
            if (hitCount > 0)
            {
                for (var index = 0; index < hitCount; index++)
                {
                    var hit = hits[index];
                    if (hit.collider.TryGetComponent(out IHitable hitable))
                    {
                        var knockbackForce = hitable.GetKnockbackForce();
                        hitable.Hit();
                        Knockback(knockbackForce);
                        break;
                    }
                }
            }
        }
    }

    private void Knockback(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        knockbacktimer = knockbacktimerSet;
        jumpTimer = 0;
    }

    private bool IsTouchingDeadZone()
    {
        return Physics2D.Raycast(footTransform.position, Vector2.down, 0.1f, deadZoneLayer);
    }

    void Restart()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(footTransform.position, Vector2.down, 0.1f, groundLayer);
    }

    public void Die()
    {
        GameManager.Instance.Die();
    }
}