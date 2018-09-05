using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D player;
    private float horizontal;

    public float speed;
    public float maxSpeed = 3;
    public float jumpPower = 150f;
    public bool grounded;
    public bool doubleJump;
    public float decayRate;

    // Use this for initialization
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        //Move player
        player.AddForce((Vector2.right * speed) * horizontal);

        #region Limiting Speed
        //Limiting the speed of the player
        if (player.velocity.x > maxSpeed)
        {
            player.velocity = new Vector2(maxSpeed, player.velocity.y);
        }

        if (player.velocity.x < -maxSpeed)
        {
            player.velocity = new Vector2(-maxSpeed, player.velocity.y);
        }

        if (player.velocity.magnitude < .01)
        {
            player.velocity = Vector3.zero;
        }
        #endregion
    }

    void Update()
    {
        #region Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                player.AddForce(Vector2.up * jumpPower);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                player.AddForce(Vector2.up * jumpPower);
                doubleJump = false;
            }

        }

        #endregion
        #region Boost
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Blink());
        }
        #endregion
    }

    IEnumerator Blink()
    {
        speed = speed * 8;
        yield return new WaitForSeconds(0.1f);
        speed = speed / 8;
    }

}
