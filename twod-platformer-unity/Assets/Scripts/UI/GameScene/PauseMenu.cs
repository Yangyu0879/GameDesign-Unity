using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePause=false;
    public GameObject pauseMenuUI;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player");
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
        //恢复钩子使用
        player.GetComponent<throwhook>().enabled = true;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePause = false;
    }
    public void PauseGame()
    {
        //禁用钩子
        player.GetComponent<throwhook>().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePause = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        isGamePause = false;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
