using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    //flash when damaged
    public float flashTime = 0.2f;

    public float knockbackFromWep = 2500f;

    public int enemyScore;


    private SpriteRenderer rendr;
    private Color originalColor;
    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    protected void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rendr = GetComponent<SpriteRenderer>();
        originalColor = rendr.color;
        //init
    }

    // Update is called once per frame
    protected void Update()
    {
        //CheckDeath
        if (hp <= 0)
        {
            ScoreBoard.AddScore(enemyScore);
            Destroy(gameObject);
        }
    }

    //be damaged
    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;
        FlashColor();
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
    private void OnTriggerEnter2D(Collider2D collision)
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
