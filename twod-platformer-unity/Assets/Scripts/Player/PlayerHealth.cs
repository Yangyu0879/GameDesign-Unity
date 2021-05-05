using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp_max = 20;
    public int hp;
    public float dieTime = 1.0f;
    public GameObject deathMenuUI;

    private SpriteRenderer rendr;
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        hp = hp_max;
        HealthBar.healthMax = hp_max;
        HealthBar.healthCurrent = HealthBar.healthMax;
        rendr = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        
        hp = Mathf.Max(0, hp - damage);
        HealthBar.healthCurrent = hp;
        if(hp <= 0)
        {
            myAnim.SetTrigger("Die");
            Invoke("PlayerDeath", dieTime);
        }
        myAnim.SetTrigger("Hit");
    }

    public void RecoverPlayer(int recovery)
    {
        hp = Mathf.Min(hp_max, hp + recovery);
        HealthBar.healthCurrent = hp;
    }

    //death
    void PlayerDeath()
    {        
        myAnim.SetTrigger("Die");
        //Destroy(gameObject);
        deathMenuUI.SetActive(true);
        
    }
}
