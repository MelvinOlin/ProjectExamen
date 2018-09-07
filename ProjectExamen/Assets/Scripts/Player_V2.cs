using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_V2 : MonoBehaviour {

    //Input Var
    private bool inputLeft;
    private bool inputRight;
    private bool inputJump;
    private bool blink;


    //Ref
    private Rigidbody2D rb2d;


    //Var for Moving Left and Right
    public float speed;
    private float maxSpeed = 3;
    private float scaleX;
    private float scaleY;

    //Var for Jumping



    //Var for Wall



    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
	}
	
	void Update () {
        MovmentController();
	}

    private void FixedUpdate()
    {
        
    }

    private void MovmentController()
    {
        if (Input.GetButtonDown("Left"))
        {
            inputLeft = true;
        }
        if (Input.GetButtonDown("Right"))
        {
            inputRight = true;
        }
        if (Input.GetButton("Jump"))
        {
            inputJump = true;
        }
    }
}
