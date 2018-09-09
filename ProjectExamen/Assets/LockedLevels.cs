using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject lock_Level_Two = GameObject.Find("Level_Lock_Two");
        GameObject lock_Level_Three = GameObject.Find("Level_Lock_Three");
        GameObject lock_Level_Four = GameObject.Find("Level_Lock_Four");

        if (!GameController.gameController.level_Two_Unlocked)
        {
            lock_Level_Two.SetActive(true);
        }
        if (!GameController.gameController.level_Three_Unlocked)
        {
            lock_Level_Three.SetActive(true);
        }
        if (!GameController.gameController.level_Four_Unlocked)
        {
            lock_Level_Four.SetActive(true);
        }
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
