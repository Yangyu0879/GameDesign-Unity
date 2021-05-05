using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoardMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject scoresMenuUI;
    public void ReturnToMainMenu()
    {
        scoresMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
