using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D player;

    float horizontal;
    public float speed;
    public float maxSpeed = 3;
    public float jumpPower = 150f;
    public bool grounded;
    public bool doubleJump;

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
    }

    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            horizontal = horizontal * 2;
            var localVelocity = transform.InverseTransformDirection(player.velocity);
            transform.position = new Vector2(transform.position.x + horizontal, transform.position.y);

            //StartCoroutine(Blink());
        }

    }

    //IEnumerator Blink()
    //{
    //    speed = speed * 40;
    //    yield return new WaitForSeconds(0.01f);
    //    speed = speed / 40;
    //}


}
