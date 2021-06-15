using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage;

    //flash when damaged
    public float flashTime = 0.2f;
    //受伤粒子特效
    public GameObject hitSparkEffect;
    public float knockbackFromWep = 2500f;

    public int enemyScore;

    private SpriteRenderer rendr;
    private Color originalColor;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    public void Start()
    {
        health = 4;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rendr = GetComponent<SpriteRenderer>();
        originalColor = rendr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        DetectAndDestroy();
    }

    protected virtual void DetectAndDestroy(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    //be damaged
    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        FlashColor();
        Instantiate(hitSparkEffect, transform.position, Quaternion.identity);
    }

    //flash
    void FlashColor()
    {
        rendr.color = Color.red;
        Invoke("ResetColor", flashTime);
    }

    //reset
    void ResetColor()
    {
        rendr.color = originalColor;
    }

    //Touch player and give damage
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString() == "UnityEngine.PolygonCollider2D"
           && collision.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                if (!playerHealth.IsDead)
                {
                    playerHealth.DamagePlayer(damage);
                }
            }
        }
    }
}
