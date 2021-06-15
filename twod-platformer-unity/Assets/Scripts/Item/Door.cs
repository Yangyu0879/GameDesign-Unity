using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{

    public GameObject KeySprite;
    private bool isDoor;

    void Start()
    {
        KeySprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDoor && Input.GetKeyDown(KeyCode.B))
        {
            EnterDoor();
        }
    }

    void EnterDoor()
    {

        if (isDoor)
        {
            if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
            {
                int i = Random.Range(2, 4);
                Debug.Log(i);
                SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
            }
            else 
            {
                int i = Random.Range(1, 3);
                SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + i);
            }
        }
    }

    public int target;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = true;
            KeySprite.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = false;
            KeySprite.SetActive(false);
        }
    }
}
