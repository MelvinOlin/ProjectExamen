using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore_Controller : MonoBehaviour {

    public GameObject mainMenu;

    public Text level_One;
    public Text level_Two;
    public Text level_Three;
    public Text level_Four;
    // Use this for initialization
    void Start () {
        level_One.text = GameController.gameController.level_HighScore_Time[1].ToString();
        level_Two.text = GameController.gameController.level_HighScore_Time[2].ToString();
        level_Three.text = GameController.gameController.level_HighScore_Time[3].ToString();
        level_Four.text = GameController.gameController.level_HighScore_Time[4].ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
