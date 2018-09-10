using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RymdRaket : MonoBehaviour
{
    public LevelController levelController;
    private Rigidbody2D rb2d;
    // Use this for initialization
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelController.canEnterShip)
        {
            levelController.Win();
            rb2d.AddForce(Vector2.up * 1000);
            rb2d.AddForce(Vector2.up * 500);
        }
    }
}
