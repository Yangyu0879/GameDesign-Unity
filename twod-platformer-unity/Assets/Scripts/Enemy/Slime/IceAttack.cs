using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAttack : MonoBehaviour
{
    public Vector3 dir;
    public float bot;
    public float velocity = 3.0f;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(dir * Time.deltaTime, Space.World);
        checkAndDestroy();
    }

    private void checkAndDestroy()
    {
        if (transform.position.y < bot)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Ice Attack Collid:"+other.tag);
        if (other.CompareTag("Player")) {
            player.GetComponent<PlayerHealth>().DamagePlayer(1);
            Destroy(gameObject);
        }
    }
}
