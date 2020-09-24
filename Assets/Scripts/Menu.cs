using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool isFirstTutorial;
    public GameObject levelList;
    public GameObject levelEntry;
    public List<string> levels;

    private void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            Debug.Log(levels[i]);
            string levelName = levels[i];
            GameObject newEntry = Instantiate(levelEntry);
            newEntry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -i * 100);
            if (isFirstTutorial)
            {
                if (i == 0)
                {
                    newEntry.transform.Find("LevelName").GetComponent<Text>().text = "Tutorial";
                }
                else
                {
                    newEntry.transform.Find("LevelName").GetComponent<Text>().text = "Level " + i;
                }
            }
            else
            {
                newEntry.transform.Find("LevelName").GetComponent<Text>().text = "Level " + (i + 1);
            }

            if (i == 0 || PlayerPrefs.GetInt(levels[i - 1] + "_finished", -1) != -1)
            {
                newEntry.transform.Find("PlayButton").gameObject.SetActive(true);
                newEntry.transform.Find("PlayButton").GetComponent<Button>().onClick.AddListener(() => LoadLevel(levelName));
                newEntry.transform.Find("Time").GetComponent<Text>().text = "";
            }

            if (PlayerPrefs.GetInt(levels[i] + "_finished", -1) != -1) {
                float time = float.Parse(PlayerPrefs.GetString(levels[i] + "_time"));
                int minutes = Mathf.FloorToInt(time / 60F);
                int seconds = Mathf.FloorToInt(time - minutes * 60);
                string niceTime = string.Format("{0:00}:{1:00}", minutes, seconds);
                newEntry.transform.Find("Time").GetComponent<Text>().text = niceTime;
            }
            newEntry.transform.SetParent(levelList.transform, false);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(levels[0]);
    }

    public void Credits()
    {
        Debug.Log("Credits, yaay!");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
