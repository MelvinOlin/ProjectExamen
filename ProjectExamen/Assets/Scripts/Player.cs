using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D player;

    float horizontal;
    public float speed = 50f;
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


        horizontal = Input.GetAxis("Horizontal");

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


    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            player.AddForce(Vector2.up * jumpPower);
        }
    }


}
