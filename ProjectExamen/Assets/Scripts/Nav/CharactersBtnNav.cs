using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharactersBtnNav : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject characters;

    public int index = 0;
    public int totalOptions = 2;
    public float xOffset = 1;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (index > 0)
            {
                index--;
                Vector2 position = transform.position;
                position.x -= xOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (index < totalOptions - 1)
            {
                index++;
                Vector2 position = transform.position;
                position.x += xOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (index)
            {
                case 0:
                    GameController.gameController.selectedCharacter = 1;
                    mainMenu.SetActive(true);
                    characters.SetActive(false);
                    break;
                case 1:
                    GameController.gameController.selectedCharacter = 2;
                    mainMenu.SetActive(true);
                    characters.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
