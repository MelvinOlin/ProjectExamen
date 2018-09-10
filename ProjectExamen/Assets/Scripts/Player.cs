using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public LayerMask wallLayerMask;
    public Transform wallCheckPoint;
    internal Rigidbody2D rb2d;
    private Animator animator;

    //Stats
    private float horizontal;
    private float jumpTimeCounter;
    private float scaleX;
    private float scaleY;
    public float speed;
    public float maxSpeed = 3;
    public float jumpPower;
    public float jumpTime;
    public float directionAxis;
    public float currentAxis;

    //Bool
    public bool isJumping;
    public bool grounded;
    public bool doubleJump;
    public bool wallSliding;
    public bool wallCheck;
    public bool facingRight = true;
    public bool wallJump;
    public bool movedInAir = false;
    public bool died;

    public bool jumpButton;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }
    //If (Wallslide)
    //      Walljump jump
    //Else 
    //      Normal jump

    void FixedUpdate()
    {
        if (grounded || wallSliding)
        {
            rb2d.gravityScale = 4;
        }
        #region Move Player

        horizontal = Input.GetAxisRaw("Horizontal");
        currentAxis = horizontal;
        if (movedInAir && !wallJump)
        {
            directionAxis = currentAxis * -1;
        }

        if (!grounded)
        {
            if (directionAxis == horizontal)
            {
                rb2d.AddForce((Vector2.right * speed) * horizontal, ForceMode2D.Force);
            }
            else
            {
                rb2d.AddForce((Vector2.right * speed / 5) * horizontal, ForceMode2D.Force);
                movedInAir = true;
            }
        }
        else
        {
            rb2d.AddForce((Vector2.right * speed) * horizontal, ForceMode2D.Force);

        }


        //Flips Player
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


        #endregion
        #region WallSlide

        if (!grounded)
        {
            wallCheck = Physics2D.OverlapCircle(wallCheckPoint.position, 0.1f, wallLayerMask);
            if (wallCheck && horizontal != 0)
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
        #region Jump
        if (jumpButton && grounded)
        {
            GetCurrentAxis();
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.velocity = Vector2.up * jumpPower;
        }

        else if (jumpButton && wallSliding)
        {
            Debug.Log("Wall Jump");

            wallJump = true;
            isJumping = true;
            //GetCurrentAxis();
            directionAxis = directionAxis * -1;
            jumpTimeCounter = jumpTime;
            rb2d.AddForce(Vector2.right * 2000);
            rb2d.velocity = Vector2.up * jumpPower * 10;
        }

        else if (Input.GetButton("Jump"))
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
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetBool("WallSliding", wallSliding);

        if (Input.GetButtonDown("Jump"))
        {
            float timer = 10f;
            jumpButton = true;
            while (timer >= 0)
            {
                timer -= Time.deltaTime;
                Debug.Log(timer);
            }
            jumpButton = false;

        }

        //Sets gravity to standard

        #region Boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Blink());
        }
        #endregion
    }

    void HandleWallSliding()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -1f);
    }

    IEnumerator Blink()
    {
        if (movedInAir)
        {
            rb2d.gravityScale = 0;
            speed = speed * 35;
            yield return new WaitForSeconds(0.06f);
            rb2d.gravityScale = 4;
            speed = speed / 35;
        }

        else
        {
            rb2d.gravityScale = 0;
            speed = speed * 6;
            yield return new WaitForSeconds(0.06f);
            rb2d.gravityScale = 4;
            speed = speed / 6;
        }
    }

    public void GetCurrentAxis()
    {
        float returnValue = Input.GetAxis("Horizontal");
        if (returnValue < 0)
        {
            returnValue = -1;
        }
        if (returnValue > 0)
        {
            returnValue = 1;
        }
        if (wallJump)
        {
            //returnValue = returnValue * -1;
        }
        directionAxis = returnValue;
    }
}
