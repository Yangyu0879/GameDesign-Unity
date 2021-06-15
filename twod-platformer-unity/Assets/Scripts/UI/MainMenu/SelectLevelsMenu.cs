using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelsMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelsMenuUI;
    public void GoToLevel1()
    {
        SceneManager.LoadScene("Level1-2");
    }
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Level2-1");
    }
    public void GoToLevel3()
    {
        SceneManager.LoadScene("Level3-1");
    }
    public void ReturnToMainMenu()
    {
        levelsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
