using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenuBtnNav : MonoBehaviour
{

    public int index = 0;
    public int totalOptions;
    public float yOffset;

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
                    Restart();
                    GameManager_Script.instance.menu.SetActive(false);
                    break;
                case 1:
                    SceneManager.LoadScene("Scene_MainMenu");
                    Time.timeScale = 1;
                    break;
                default:
                    break;
            }
        }
    }

    void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }
}
