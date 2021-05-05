using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public void ReturnToMainMenu()
    {
        settingsMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
