using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public Player player;

    public AudioSource jump;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && player.grounded || Input.GetButton("Jump") && player.wallJump)
        {
            jump.Play();
        }
    }
}
