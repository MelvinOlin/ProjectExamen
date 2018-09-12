using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject intro;


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mainMenu.SetActive(true);
            intro.SetActive(false);
        }
	}
}
