using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectBtnNav : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject levelSelect;

	public int index = 0;
	public int totalOptions = 3;
	public float yOffset = 1;
	
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
					SceneManager.LoadScene("Level_One");
					break;
                case 1:
                    if (GameController.gameController.level_Unlocked[2])
                    {
                        SceneManager.LoadScene("Level_Two");
                    }
                    break;
                case 2:
                    if (GameController.gameController.level_Unlocked[3])
                    {
                        SceneManager.LoadScene("Level_Three");
                    }
                    break;
                case 3:
                    if (GameController.gameController.level_Unlocked[4])
                    {
                        SceneManager.LoadScene("Level_Four");
                    }
                    break;
                case 4:
                    levelSelect.SetActive(false);
                    mainMenu.SetActive(true);
                    break;

				default:
					break;
			}
		}
	}
}
