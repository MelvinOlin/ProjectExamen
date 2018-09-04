using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.grounded = true;
        player.doubleJump = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
