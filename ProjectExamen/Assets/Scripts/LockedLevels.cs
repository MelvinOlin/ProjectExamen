using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedLevels : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject lock_Level_Two = GameObject.Find("Level_Lock_Two");
        GameObject lock_Level_Three = GameObject.Find("Level_Lock_Three");
        GameObject lock_Level_Four = GameObject.Find("Level_Lock_Four");

        if (!GameController.gameController.level_Unlocked[2])
        {
            lock_Level_Two.SetActive(true);
        }
        else
        {
            lock_Level_Two.SetActive(false);
        }

        if (!GameController.gameController.level_Unlocked[3])
        {
            lock_Level_Three.SetActive(true);
        }
        else
        {
            lock_Level_Three.SetActive(false);

        }
        if (!GameController.gameController.level_Unlocked[4])
        {
            lock_Level_Four.SetActive(true);
        }
        else
        {
            lock_Level_Three.SetActive(false);

        }
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
