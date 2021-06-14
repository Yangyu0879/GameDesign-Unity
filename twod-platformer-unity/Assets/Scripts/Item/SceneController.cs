using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController
{
    public static SceneController _instance;

    public static SceneController Instance
    {
        get 
        {
            if (_instance == null) {
                _instance = new SceneController();
            }
            return _instance;
        }
    }

    public AsyncOperation asyncOperation;

    public void LoadScene(int target) 
    {
        //œ‘ æ


        //
        asyncOperation = SceneManager.LoadSceneAsync(target);
    }
}
