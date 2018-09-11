using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

    public AudioSource jump;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump"))
        {
            jump.Play();
        }
    }
}
