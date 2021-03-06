﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedNav : MonoBehaviour
{
    public Scene nextScene;

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
        //Om man trycker w elr up
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (index > 0)
            {
                index--;
                Vector2 position = transform.position; //nuvarande pos
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
                    LevelController.instance.pausedMenu.SetActive(false);
                    break;
                case 1:
                    SceneManager.LoadScene(nextScene.name);
                    break;
                case 2:
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
