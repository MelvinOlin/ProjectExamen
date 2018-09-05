using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBtnNav : MonoBehaviour {

	public int index = 0;
	public int totalOptions = 3;
	public float yOffset = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.W))
		{
			if (index > 0)
			{
				index--;
				Vector2 position = transform.position;
				position.y += yOffset;
				transform.position = position;
			}
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			if (index < totalOptions - 1)
			{
				index++;
				Vector2 position = transform.position;
				position.y -= yOffset;
				transform.position = position;
			}
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			switch (index)
			{
				case 0:
					SceneManager.LoadScene("Scene_Test");
					break;
				default:
					break;
			}
		}
	}
}
