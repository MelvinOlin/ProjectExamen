using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtnNav : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject levelSelect;

    public int index = 0;
    public int totalOptions = 3;
    public float yOffset = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
                    levelSelect.SetActive(true);
                    mainMenu.SetActive(false);
                    break;
                case 1:
                    break;
                case 2:
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
}