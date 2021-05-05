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

    }
    public void GoToLevel2()
    {

    }
    public void GoToLevel3()
    {

    }
    public void ReturnToMainMenu()
    {
        levelsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
