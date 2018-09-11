using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;

    public GameObject plr;
    public Player player;
    public GameObject pausedMenu;
    public Canvas stats;
    public GameObject winScreen;    
    public GameObject gameOverScreen;
    Text babiesTaken;
    Text babiesTotal;
    Text timer;

    public int level;
    public int babyCount;
    public int babyCountTaken;
    public bool paused = false;
    public float timeLeft;
    public float startTime;
    bool timesUp = false;
    public bool canEnterShip;
    bool won;
    public float minHeightPlayer;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        startTime = timeLeft;
        babiesTaken = stats.transform.Find("Taken").GetComponent<Text>();
        babiesTotal = stats.transform.Find("Total").GetComponent<Text>();
        timer = stats.transform.Find("Timer").GetComponent<Text>();
    }

    void Update()
    {
        babiesTotal.text = babyCount.ToString();
        babiesTaken.text = babyCountTaken.ToString();
        if (!won && player.died || plr.transform.localPosition.y < minHeightPlayer)
        {
            GameOver();
        }

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
            canEnterShip = true;
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
            pausedMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
        else
        {
            pausedMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void Win()
    {
        Debug.Log("WIN");
        float timeCompleted = startTime - timeLeft;

        if (timeCompleted < GameController.gameController.level_HighScore_Time[level] || GameController.gameController.level_HighScore_Time[level] <= 0)
        {
            GameController.gameController.level_HighScore_Time[level] = timeCompleted;
        }
        if (level < GameController.gameController.level_Unlocked.Length + 1)
        {
            GameController.gameController.level_Unlocked[level + 1] = true;
        }
        double times = timeCompleted;
        times = System.Math.Round(times, 2);

        Text time = winScreen.transform.Find("Time").GetComponent<Text>();
        time.text = times.ToString();

        Text highScoreTIme = winScreen.transform.Find("HighScoreTime").GetComponent<Text>();

        double highScoreTimes = GameController.gameController.level_HighScore_Time[level];
        highScoreTimes = Math.Round(highScoreTimes, 2);

        highScoreTIme.text = highScoreTimes.ToString();

        GameController.gameController.Save();
        plr.SetActive(false);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}