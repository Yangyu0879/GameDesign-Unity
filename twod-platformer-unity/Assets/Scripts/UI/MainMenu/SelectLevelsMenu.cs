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
        int i = Random.Range(1, 3);
        SceneManager.LoadScene("Level1-"+i.ToString());
    }
    public void GoToLevel2()
    {
        int i = Random.Range(1, 3);
        SceneManager.LoadScene("Level2-" + i.ToString());
    }
    public void GoToLevel3()
    {
        int i = Random.Range(1, 3);
        SceneManager.LoadScene("Level3-"+i.ToString());
    }
    public void ReturnToMainMenu()
    {
        levelsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
