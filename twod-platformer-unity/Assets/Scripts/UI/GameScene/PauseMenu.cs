using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePause=false;
    public GameObject pauseMenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePause = false;
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePause = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        isGamePause = false;
        //save score
        ScoreBoard.SaveScore();
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        //save score
        ScoreBoard.SaveScore();
        Application.Quit();
    }
}
