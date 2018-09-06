using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask wallLayerMask;
    public Transform wallCheckPoint;
    internal Rigidbody2D rb2d;

    //Stats
    private float horizontal;
    private float jumpTimeCounter;
    private float scaleX;
    private float scaleY;
    public float speed;
    public float maxSpeed = 3;
    public float jumpPower;
    public float jumpTime;

    //Bool
    private bool isJumping;
    public bool grounded;
    public bool doubleJump;
    public bool wallSliding;
    public bool wallCheck;
    public bool facingRight = true;
    public bool wallJump;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }


    void FixedUpdate()
    {
        #region Move Player
        if (!wallJump)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb2d.AddForce((Vector2.right * speed) * horizontal);
            if (horizontal < -0.1f)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 1);
                facingRight = false;
            }
            if (horizontal > 0.1f)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 1);
                facingRight = true;
            }
        }
        #endregion
        #region Limiting Speed
        //Limiting the speed of the player
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.magnitude < .01)
        {
            rb2d.velocity = Vector3.zero;
        }
        #endregion
    }

    void Update()
    {
        //Sets gravity to standard
        if (grounded || wallSliding)
        {
            rb2d.gravityScale = 4;
        }

        #region Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.velocity = Vector2.up * jumpPower;
        }

        if (Input.GetButtonDown("Jump") && wallSliding)
        {
            Debug.Log("Wall Jump");
            isJumping = true;
            rb2d.AddForce(Vector2.right * jumpPower, ForceMode2D.Impulse); 
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButton("Jump"))
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            rb2d.gravityScale = 6;
            isJumping = false;
        }



        #endregion
        #region Boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Blink());
        }
        #endregion
        #region WallSlide

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);
            if (wallCheck)
            {
                wallSliding = true;
                HandleWallSliding();
            }
        }

        if (!wallCheck || grounded)
        {
            wallSliding = false;
        }

        #endregion

    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -1f);
    }

    IEnumerator Blink()
    {
        rb2d.gravityScale = 0;
        speed = speed * 6;
        yield return new WaitForSeconds(0.06f);
        rb2d.gravityScale = 4;
        speed = speed / 6;
    }

    IEnumerator WallJump()
    {
        wallJump = true;
        yield return new WaitForSeconds(0.5f);
        wallJump = false;
    }

}
