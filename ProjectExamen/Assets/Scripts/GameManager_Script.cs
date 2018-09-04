using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script instance;

    public Player player;
    public int babyCount;
    public int babyCountTaken;

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
    }
}
