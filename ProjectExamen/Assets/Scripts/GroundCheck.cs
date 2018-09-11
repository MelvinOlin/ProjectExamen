using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private Player player;

    void Start()
    {
        player = gameObject.GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {

            player.jumped = false;
            player.isJumping = false;
            player.GetCurrentAxis();
            player.wallJump = false;
            player.movedInAir = false;
            player.directionAxis = 0;
            player.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
        if (!player.isJumping)
        {
            player.GetCurrentAxis();
        }
    }
}
