using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoardMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject scoresMenuUI;
    public Text[] scoreTextList;
    
    public void ReloadScoreList()
    {
        //if there is no such key, return default val 0
        int scoreNo1 = PlayerPrefs.GetInt("SCORE_NO1");
        int scoreNo2 = PlayerPrefs.GetInt("SCORE_NO2");
        int scoreNo3 = PlayerPrefs.GetInt("SCORE_NO3");
        int scoreNo4 = PlayerPrefs.GetInt("SCORE_NO4");
        int scoreNo5 = PlayerPrefs.GetInt("SCORE_NO5");
        //set
        scoreTextList[0].text = scoreNo1.ToString("D5");
        scoreTextList[1].text = scoreNo2.ToString("D5");
        scoreTextList[2].text = scoreNo3.ToString("D5");
        scoreTextList[3].text = scoreNo4.ToString("D5");
        scoreTextList[4].text = scoreNo5.ToString("D5");
    }

    public void ReturnToMainMenu()
    {
        scoresMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
