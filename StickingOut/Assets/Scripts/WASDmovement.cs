using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NewMonoBehaviourScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private bool isFacingRight = true;
    public bool canJump = true;
    public float jumpValue = 0.0f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey("space") && IsGrounded() && canJump)
        {
            jumpValue += 0.2f;
        }

        if(Input.GetKeyDown("space") && IsGrounded() && canJump)
        {
            rb.linearVelocity = new Vector2(0.0f, rb.linearVelocity.y);
        }

        if (jumpValue >= 20f && IsGrounded())
        {
            float tempx = horizontal * speed;
            float tempy = jumpValue;
            rb.linearVelocity = new Vector2 (tempx, tempy);
            ResetJump();
        }

        if (Input.GetKeyUp("space"))
        {
            if(IsGrounded())
            {
                rb.linearVelocity = new Vector2(horizontal * speed, jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }

        void ResetJump()
        {
            canJump = false;
            jumpValue = 0;
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
