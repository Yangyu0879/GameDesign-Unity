using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        //save score
        ScoreBoard.SaveScore();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.PlayerRestart();
    }
    public void MainMenu()
    {
        //save score
        ScoreBoard.SaveScore();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        //save score
        ScoreBoard.SaveScore();
        Application.Quit();
    }
}
