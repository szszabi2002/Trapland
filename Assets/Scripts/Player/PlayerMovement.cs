﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Rigidbody2D rb;
    private Animator anim;
    private float horizontalInput;
    private bool isFacingRight = true;

    [Header("Wall Jump")]
    [SerializeField] private Transform WallCheck;
    private bool canGrab, IsGrabbing;
    private float gravityStore;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [Header("Jump Buffer")]
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float JumpBufferTime;
    
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gravityStore = rb.gravityScale;
    }
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(Grounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump"))
        {
            JumpBufferTime = jumpBufferTime;
        }
        else
        {
            JumpBufferTime -= Time.deltaTime;
        }
        if (JumpBufferTime>0f && coyoteTimeCounter > 0f)
        {
            anim.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            JumpBufferTime = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        Flip();
        
        canGrab = Physics2D.OverlapCircle(WallCheck.position, 0.2f, groundLayer);
        IsGrabbing = false;
        if (canGrab && !IsGrounded())
        {
            if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0f) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0f))
            {
                IsGrabbing = true;
            }
        }
        /*if (IsGrabbing)
        {
            //rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = gravityStore;
        }*/
    }
    private void FixedUpdate()
    {
        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
