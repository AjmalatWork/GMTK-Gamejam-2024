using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float swingSpeedMultiplier;

    private float moveInput;
    private Rigidbody2D rb;
    private WallJump wallJump;
    private ExtendArm arm;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        wallJump = GetComponent<WallJump>();
        arm = GetComponentInChildren<ExtendArm>();
        animator = GetComponent<Animator>();
    }

    public void HandleMovement()
    {
        if(!wallJump.isWallJumping)
        {
            moveInput = InputManager.Instance.GetHorizontalInput();
            float targetSpeed = moveSpeed * moveInput;

            animator.SetFloat("Speed", Mathf.Abs(targetSpeed));

            if (arm.isArmAttached)
            {
                targetSpeed = targetSpeed * swingSpeedMultiplier;
            }

            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);

            TurnPlayer();            
        }        
    }

    private void TurnPlayer()
    {        
        // transform.localscale.x is used to get the direction the player is facing
        // Right means 1 and Left means -1
        if ((transform.localScale.x > 0f && moveInput < 0) || (transform.localScale.x < 0f && moveInput > 0))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
        }
    }
}
