using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        //Build setting: 2-SelectLevels
        SceneManager.LoadScene(2);
    }
    public void Settings()
    {

    }
    public void ScoreBoard()
    {

    }
}
