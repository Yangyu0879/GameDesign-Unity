using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public GameObject coin;
    public GameObject KeySprite;
    public float delayTime;
    public int coinCnt;
    public float coinUpSpeed;
    public float intervalTime;
    private bool canOpen;
    private bool isOpened;
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        KeySprite.SetActive(false);
        isOpened = false;
    }

    IEnumerator GenCoins()
    {
        WaitForSeconds wait = new WaitForSeconds(intervalTime);
        for (int i = 0; i < coinCnt; ++i)
        {
            GameObject nowcoin = Instantiate(coin, transform.position, Quaternion.identity);
            Vector2 randomDir = new Vector2(Random.Range(-0.5f, 0.5f), 1.0f);
            nowcoin.GetComponent<Rigidbody2D>().velocity = randomDir * coinUpSpeed;
            yield return wait;
        }
    }


            // Update is called once per frame
    void Update()
    {
        Debug.Log(canOpen);
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(canOpen&&!isOpened)
            {
                myAnim.SetBool("opening", true);
                isOpened = true;
                //Invoke("GenCoin", delayTime);
                StartCoroutine(GenCoins());
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = true;
            if(!isOpened)
            {
                KeySprite.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            canOpen = false;
            KeySprite.SetActive(false);
        }
    }
}
