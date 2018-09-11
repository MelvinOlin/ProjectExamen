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
    private float horizontal;        //Checks PLAYERS DIRECTION
    private float jumpTimeCounter;   //COUNTS DOWN ftom jumpTime to 0
    public float jumpTime;           //Jumptimer for controllable jump height
    private float scaleX;            //For FLIPPING PLAYER
    private float scaleY;            //For FLIPPING PLAYER
    public float speed;              //Player speed
    public float maxSpeed = 3;       //Limit player speed
    public float jumpPower;          //Controlls player JUMP POWER
    public float directionAxis;      //Show what way player was when jumping
    public float currentAxis;        //Show what way player is facing
    public float jumpButtonTimer;    //Get JUMP INPUT from UPDATE to FIXEDUPDATE

    //Bool
    public bool jumped;              //check if PLAYER have jumped to disable unintended DOUBLE JUMP
    public bool isJumping;           //TRUE if PLAYER is JUMPING
    public bool grounded;            //TRUE if PLAYER is GROUNDED
    public bool wallSliding;         //TRUE if PLAYER is WALLSLIDING
    public bool wallCheck;           //TRUE if PLAYER is close to WALL
    public bool facingRight;         //self explanatory
    public bool wallJump;            //TRUE if PLAYER JUMP from WALL
    public bool movedInAir;          //TRUE if PLAYER MOVED in air
    public bool canControll;         //cant controll PLAYER for 0.1sec if WALLJUMP
    public bool canBlink;            //Checks if BLINK have Cooldown
    public bool canWallJump;         //Cooldown for walljump to disable walljump spam
    public bool died;                //TRUE if PLAYER DIED

    public bool jumpButton;

    // Use this for initialization
    void Start()
    {
        canWallJump = true;
        canControll = true;
        canBlink = true;
        rb2d = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    void FixedUpdate()
    {
        if (grounded || wallSliding)
        {
            rb2d.gravityScale = 4;
            jumped = false;
        }
        #region Move Player
        if (canControll)
        {
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

        if (jumpButtonTimer > 0)
        {
            jumpButtonTimer -= Time.deltaTime;
        }
        else
        {
            jumpButton = false;
        }

        if (jumpButton && grounded && !jumped)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb2d.velocity = Vector2.up * jumpPower;
        }

        else if (jumpButton && wallSliding)
        {
            Debug.Log("WALLJUMP");
            wallJump = true;
            isJumping = true;
            jumped = true;
            GetCurrentAxis();
            directionAxis = directionAxis * -1;
            jumpTimeCounter = jumpTime;

            StartCoroutine(WallJumpTimer());
            StartCoroutine(WallJumpCoolDown());

            rb2d.velocity = Vector3.zero;
            rb2d.AddForce(new Vector3(1000 * directionAxis, 1000, 0), ForceMode2D.Force);
        }

        else if (Input.GetButton("Jump") && !wallSliding && !jumped && canWallJump && !wallJump)
        {
            Debug.Log("BUG");
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
            jumped = true;
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
            GetCurrentAxis();
            jumpButtonTimer = 0.05f;
            jumpButton = true;
        }
        #region Blink
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canBlink)
            {
                StartCoroutine(Blink());
            }
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
        StartCoroutine(BlinkCoolDown());
    }

    public void GetCurrentAxis()
    {
        float returnValue = Input.GetAxis("Horizontal");
        if (returnValue < 0)
        {
            returnValue = -1;
        }
        else if (returnValue > 0)
        {
            returnValue = 1;
        }
        directionAxis = returnValue;
    }

    IEnumerator WallJumpTimer()
    {
        canControll = false;
        yield return new WaitForSeconds(0.1f);
        canControll = true;
    }

    IEnumerator WallJumpCoolDown()
    {
        canWallJump = false;
        yield return new WaitForSeconds(0.6f);
        canWallJump = true;
    }

    IEnumerator BlinkCoolDown()
    {
        canBlink = false;
        yield return new WaitForSeconds(6f);
        canBlink = true;
    }

}
