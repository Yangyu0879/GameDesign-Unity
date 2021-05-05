using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    public int coinScore = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"
           && collision.CompareTag("Player"))
        {
            ScoreBoard.AddScore(coinScore);
            Destroy(gameObject);
        }
    }
}
