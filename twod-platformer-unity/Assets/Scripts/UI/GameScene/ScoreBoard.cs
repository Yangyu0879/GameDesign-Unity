using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public static int currentScore = 0;
    public Text currentScoreText;
    // Start is called before the first frame update
    void Start()
    {
        //或者载入其它数据
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text = "SCORE: " + currentScore.ToString();
    }

    public static void AddScore(int addScore)
    {
        if(addScore>0)
        {
            currentScore += addScore;
        }
    }

    //save score data
    public static void SaveScore()
    {
        //if there is no such key, return default val 0
        int scoreNo1 = PlayerPrefs.GetInt("SCORE_NO1");
        int scoreNo2 = PlayerPrefs.GetInt("SCORE_NO2");
        int scoreNo3 = PlayerPrefs.GetInt("SCORE_NO3");
        int scoreNo4 = PlayerPrefs.GetInt("SCORE_NO4");
        int scoreNo5 = PlayerPrefs.GetInt("SCORE_NO5");
        //sort
        List<int> scoreList = new List<int>{ scoreNo1, scoreNo2, scoreNo3, scoreNo4, scoreNo5, currentScore };
        scoreList.Sort();
        //save
        PlayerPrefs.SetInt("SCORE_NO1", scoreList[5]);
        PlayerPrefs.SetInt("SCORE_NO2", scoreList[4]);
        PlayerPrefs.SetInt("SCORE_NO3", scoreList[3]);
        PlayerPrefs.SetInt("SCORE_NO4", scoreList[2]);
        PlayerPrefs.SetInt("SCORE_NO5", scoreList[1]);
    }
}
