using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int hp_max = 20;
    public int hp;
    public int blinksNum = 2;
    public float blinkTime = 0.1f;
    public float dieTime = 1.0f;
    public float hitBoxCDTime = 1.0f;
    public GameObject deathMenuUI;
    //restart
    private Transform restartPos;
    public Transform startPos;

    private SpriteRenderer rendr;
    private Animator myAnim;
    private PolygonCollider2D polygonCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        restartPos = startPos;
        hp = hp_max;
        HealthBar.healthMax = hp_max;
        HealthBar.healthCurrent = HealthBar.healthMax;
        rendr = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        polygonCollider2D= GetComponent<PolygonCollider2D>();
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
        StartCoroutine(Blinks(blinksNum, blinkTime));
        //无敌时间
        polygonCollider2D.enabled = false;
        StartCoroutine(SetPlayerHitBox());
    }

    IEnumerator SetPlayerHitBox()
    {
        yield return new WaitForSeconds(hitBoxCDTime);
        polygonCollider2D.enabled = true;
    }
    IEnumerator Blinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < 2 * numBlinks; i++)
        {
            rendr.enabled = !rendr.enabled;
            yield return new WaitForSeconds(seconds);
        }
        rendr.enabled = true;
    }

    public void RecoverPlayer(int recovery)
    {
        hp = Mathf.Min(hp_max, hp + recovery);
        HealthBar.healthCurrent = hp;
    }

    //death
    void PlayerDeath()
    {
        deathMenuUI.SetActive(true);
        //代替Destroy(gameObject);
        //死亡后禁用一些功能
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<throwhook>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

    }

    //restart
    public void PlayerRestart()
    {
        myAnim.SetTrigger("Restart");
        deathMenuUI.SetActive(false);       
        //将状态重置        
        transform.position = restartPos.position;
        hp = hp_max;
        HealthBar.healthCurrent = hp;
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<throwhook>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
