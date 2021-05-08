using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelsMenuUI;
    public GameObject settingsMenuUI;
    public GameObject scoresMenuUI;
    public void PlayGame()
    {
        //Play should be put at 1
        //Build setting: 1-PlayScene
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SelectLevels()
    {
        mainMenuUI.SetActive(false);
        levelsMenuUI.SetActive(true);
    }
    public void Settings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    public void ScoreBoard()
    {
        mainMenuUI.SetActive(false);
        //reload scores
        ScoreBoardMenu scoreMenu = FindObjectOfType<ScoreBoardMenu>();
        scoreMenu.ReloadScoreList();
        scoresMenuUI.SetActive(true);
    }
}
