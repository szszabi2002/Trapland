using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private float DefaultJumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    [Header("Wall Jump")]
    [SerializeField] private float wallJumpTime;
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallDistance;
    private bool isWallSliding = false;
    private RaycastHit2D WallCheckHit;
    private float jumpTime;
    private bool isFacingRight = true;
    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        DefaultJumpPower = jumpPower;
    }
    private void Update()
    {
        if (isGrounded() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) || isWallSliding && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            anim.SetTrigger("jump");
            Jump();
        }
    }
    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            isFacingRight = true;
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //Wall jump logic
        if (isFacingRight)
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, groundLayer);
        }
        else
        {
            WallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, groundLayer);
        }
        if (WallCheckHit && !isGrounded() && body.velocity.y < 0)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpTime;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
        }
        if (isWallSliding)
        {
            body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed);
            jumpPower = 20;
        }
        else
        {
            jumpPower = DefaultJumpPower;
        }
        if (isWallSliding && Input.GetKey(KeyCode.DownArrow))
        {
            body.velocity = new Vector2(body.velocity.x, -wallSlideSpeed * 10);
            isWallSliding = false;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
        
    }
    public bool canAttack()
    {
        return !isWallSliding;
    }
}