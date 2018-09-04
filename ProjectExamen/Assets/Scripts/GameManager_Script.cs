using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script instance;

    public Player player;
    public GameObject menu;
    public int babyCount;
    public int babyCountTaken;
    public bool paused = false;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("_Scene_Test");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                menu.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }
            else
            {
                menu.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
        }
    }
}
