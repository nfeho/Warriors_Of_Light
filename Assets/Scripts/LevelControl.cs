using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private GameObject gameOverPanel;

    public GameObject light;
    public GameObject dark;
    public PlayerFell playerFell;

    private System.DateTime startTime;

    void Start()
    {
        pausePanel.SetActive(false);
        startTime = System.DateTime.UtcNow;
    }

    void Update()
    {
        if (gameObject.GetComponent<Finish>().everyoneReady)
        {
            if (!winnerPanel.activeSelf)
            {
                Time.timeScale = 0;
                winnerPanel.SetActive(true);
                System.TimeSpan ts = System.DateTime.UtcNow - startTime;
                double time = (System.DateTime.UtcNow - startTime).TotalSeconds;
                int minutes = (int)System.Math.Floor(time / 60);
                int seconds = (int)System.Math.Floor(time - minutes * 60);
                string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                winnerPanel.transform.Find("ContentPanel").transform.Find("TimeNumbers").GetComponent<Text>().text = niceTime;

                if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_finished", -1) == -1
                    || double.Parse(PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_time")) > time)
                {
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_finished", 1);
                    PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_time", time.ToString());
                    winnerPanel.transform.Find("ContentPanel").transform.Find("HighScore").gameObject.SetActive(true);
                }
            }
        }
        else if (!light.activeInHierarchy || !dark.activeInHierarchy || playerFell.gameOver)
        {
            if (!gameOverPanel.activeSelf)
            {
                Time.timeScale = 0;
                gameOverPanel.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                this.RestartLevel();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
