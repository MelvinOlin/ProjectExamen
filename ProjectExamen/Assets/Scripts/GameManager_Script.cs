using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script instance;

    public Player player;
    public GameObject menu;
    public Canvas stats;
    public GameObject winScreen;
    public GameObject gameOverScreen;
    Text babiesTaken;
    Text babiesTotal;
    Text timer;
    

    public int babyCount;
    public int babyCountTaken;
    public bool paused = false;
    public float timeLeft;
    public float startTime;
    bool timesUp = false;
    bool won;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        startTime = timeLeft;
        babiesTaken = stats.transform.Find("Taken").GetComponent<Text>();
        babiesTotal = stats.transform.Find("Total").GetComponent<Text>();
        timer = stats.transform.Find("Timer").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        babiesTotal.text = babyCount.ToString();
        babiesTaken.text = babyCountTaken.ToString();

        #region Timer
        if (!won)
        {
            timeLeft -= Time.deltaTime;
        }
        double timeLeftRounded = System.Math.Round(timeLeft, 0);

        if (!timesUp)
        {

            if (timeLeftRounded < 10)
            {
                timer.text = "0" + timeLeftRounded.ToString();
            }
            else
            {
                timer.text = timeLeftRounded.ToString();
            }
        }

        if (timeLeft <= 0)
        {
            timer.text = "00";
            timesUp = true;
            GameOver();
        }
        #endregion

        if (babyCount == babyCountTaken)
        {
            won = true;
            Win();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene);
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    private void Pause()
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

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
    private void Win()
    {
        float timeCompleted = startTime - timeLeft;
        Text time = winScreen.transform.Find("Time").GetComponent<Text>();
        time.text = timeCompleted.ToString();
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
