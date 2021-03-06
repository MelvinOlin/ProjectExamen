﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RymdRaket : MonoBehaviour
{
    public LevelController levelController;
    private Rigidbody2D rb2d;
    private bool launch;
    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (launch)
        {
            rb2d.AddForce(Vector2.up * 30);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelController.canEnterShip)
        {
            levelController.Win();
            rb2d.constraints = RigidbodyConstraints2D.None;
            //rb2d.AddForce(Vector2.up * 300);
            //rb2d.AddForce(Vector2.up * 500);
            launch = true;
        }
    }
}
