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
            SceneController.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
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
