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
}
