using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransDoor : MonoBehaviour
{
    public Transform transDoor;
    private Transform playerTrnas;
    private bool isDoor;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTrnas = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(isDoor&&Input.GetKeyDown(KeyCode.B))
        {
            EnterDoor();
        }
    }

    void EnterDoor()
    {
        if(isDoor)
        {
            playerTrnas.position = transDoor.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = true;
            Debug.Log(isDoor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            isDoor = false;
            Debug.Log(isDoor);
        }
    }
}
