using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed;
    public float waitSecond;
    public Transform[] movePos;

    private int index;
    private int posCnt;
    private float eps;
    private float storeWaitSecond;
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        eps = 0.1f;
        storeWaitSecond = waitSecond;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        posCnt = movePos.Length;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movePos[index].position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position,movePos[index].position)<eps)
        {
            if(waitSecond<.0f)
            {
                index = (++index) % posCnt;
                waitSecond = storeWaitSecond;
            }
            else
            {
                waitSecond -= Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&collision.GetType().ToString()== "UnityEngine.BoxCollider2D")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            collision.gameObject.transform.parent = playerTransform;
        }
    }

}
