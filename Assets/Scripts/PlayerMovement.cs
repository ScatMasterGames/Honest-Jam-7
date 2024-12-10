using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform footTransform;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask deadZoneLayer;
    
    [SerializeField] float speed = 10.0f;
    [SerializeField] float jumpSpeed = 2.0f;
    [SerializeField] float gravity = -20.0f;
    [SerializeField] float jumpTimerSet = 0.2f;

    float jumpTimer;
    bool isDead = false;
    float velocityXSmoothing;
    Vector2 targetVelocity;
    Rigidbody2D rb;
    private float fallTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.gravity = new Vector2(0, gravity);
    }

    private void Update()
    {
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

        if (jumpTimer > 0)
        {
            targetVelocityY = jumpSpeed;
        }
        else
        {
            targetVelocityY = rb.velocity.y;
        }

        targetVelocity = new Vector2(targetVelocityX, targetVelocityY);
        rb.velocity = targetVelocity;
        
        if(IsTouchingDeadZone())
        {
            isDead = true;
            Invoke(nameof(Restart), 2f);
        }
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
}