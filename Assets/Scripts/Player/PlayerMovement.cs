using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJumps;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask wall;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float wallParameterJumpX;
    [SerializeField] private float wallParameterJumpY;
    [SerializeField] private Joystick joystick;



    private bool facingRight = true;
    private float coyoteCounter;
    private int jumpCounter;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = joystick.Horizontal;

        if (horizontalInput > 0f && !facingRight)
            FlipPlayer();
        else if (horizontalInput < -0f && facingRight)
            FlipPlayer();


        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", Grounded());

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            Jump();

        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (sWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * movementSpeed, body.velocity.y);

            if (Grounded())
            {
                coyoteCounter = coyoteTime; 
                jumpCounter = extraJumps; 
            }
            else
                coyoteCounter -= Time.deltaTime; 
        }
    }


    public void Jump()
    {

        if (coyoteCounter <= 0 && !sWall() && jumpCounter <= 0) return;
        SoundManager.instance.PlaySound(jumpSound);

        if (sWall())
            WallJump();
        else
        {
            if (Grounded())
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            else
            {
                if (coyoteCounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpForce);
                else
                {
                    if (jumpCounter > 0) 
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpForce);
                        jumpCounter--;
                    }
                }
            }
            coyoteCounter = 0;
        }
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallParameterJumpX, wallParameterJumpY));
        wallJumpCooldown = 0;
    }

    private bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, ground);
        return raycastHit.collider != null;
    }
    private bool sWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wall);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && Grounded() && !sWall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
            Destroy(collision.gameObject);
    }
}