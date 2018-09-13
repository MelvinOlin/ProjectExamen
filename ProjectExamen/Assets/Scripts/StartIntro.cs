using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject intro;


    private void Start()
    {
        if (!GameController.gameController.showedIntro)
        {
            intro.SetActive(true);
            mainMenu.SetActive(false);
            GameController.gameController.showedIntro = true;
        }
        else
        {
            mainMenu.SetActive(true);
            intro.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mainMenu.SetActive(true);
            intro.SetActive(false);
        }
	}
}
