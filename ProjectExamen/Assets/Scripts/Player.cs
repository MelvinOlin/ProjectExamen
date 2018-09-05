using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask wallLayerMask;
    public Transform wallCheckPoint;
    private Rigidbody2D rb2d;

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

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }


    void FixedUpdate()
    {
        //Move player
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
        #region Jump
        if (Input.GetButtonDown("Jump") && grounded || wallSliding)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.velocity = Vector2.up * jumpPower;
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
            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f) 
            {
                if (wallCheck)
                {
                    HandleWallSliding();
                }
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
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.7f);
        wallSliding = true;
        //if (Input.GetButtonDown("Jump"))
        //{
        //    if (facingRight)
        //    {
        //        //rb2d.AddForce(new Vector2(-1000, 20) * jumpPower);
        //        rb2d.velocity = new Vector2(-2, 5) * jumpPower;
        //    }
        //    else
        //    {
        //        //rb2d.AddForce(new Vector2(1000, 20) * jumpPower);
        //        rb2d.velocity = new Vector2(2, 5) * jumpPower;


        //    }
        //}
    }

    IEnumerator Blink()
    {
        rb2d.gravityScale = 0;
        speed = speed * 8;
        yield return new WaitForSeconds(0.05f);
        rb2d.gravityScale = 4;
        speed = speed / 8;
    }

}


//if (grounded)
//{
//    player.AddForce(Vector2.up * jumpPower);
//    doubleJump = true;
//}
//else if (doubleJump)
//{
//    player.AddForce(Vector2.up * jumpPower);
//    doubleJump = false;
//}