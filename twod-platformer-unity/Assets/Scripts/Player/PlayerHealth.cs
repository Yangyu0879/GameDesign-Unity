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
    //����������Ч
    public GameObject hitSparkEffect;
    //flag of if player dead
    private bool isDead=false;
    public bool IsDead { get => isDead; }

    //restart
    private Transform restartPos;
    public Transform startPos;

    private SpriteRenderer rendr;
    private Animator myAnim;
    private PolygonCollider2D polygonCollider2D;
    private ScreenFlash screenFlash;
    private Cinemachine.CinemachineCollisionImpulseSource playerHitImpulse;
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
        screenFlash = GetComponent<ScreenFlash>();
        playerHitImpulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        
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
            //�����߼�
            if(!isDead)
            {
                //set player live state
                isDead = true;
                myAnim.SetTrigger("Die");
                Invoke("PlayerDeath", dieTime);
            }            
        }
        //������˱�ʾ
        StartCoroutine(Blinks(blinksNum, blinkTime));
        Instantiate(hitSparkEffect, transform.position, Quaternion.identity);
        //��Ļ���˱�ʾ
        screenFlash.FlashScreen();
        playerHitImpulse.GenerateImpulse(transform.position);
        //�޵�ʱ��
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

    public void RecoverPlayerAll()
    {
        hp = hp_max;
        HealthBar.healthCurrent = hp;
    }

    //death
    void PlayerDeath()
    {
        deathMenuUI.SetActive(true);
        //����Destroy(gameObject);
        //���������һЩ����
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<throwhook>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        GetComponent<PlayerController>().enabled = false;
    }

    //restart
    public void PlayerRestart()
    {
        myAnim.SetTrigger("Restart");
        deathMenuUI.SetActive(false);       
        //��״̬����        
        transform.position = restartPos.position;
        hp = hp_max;
        HealthBar.healthCurrent = hp;
        //GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<throwhook>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        GetComponent<PlayerController>().enabled = true;
        //set player live state
        isDead = false;
    }

    public void setCheckpoint(Transform now)
    {
        restartPos = now;
    }
}
