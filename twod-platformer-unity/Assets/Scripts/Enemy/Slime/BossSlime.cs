using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime :  Enemy
{
    public GameObject[] initiatePos;
    public GameObject slime;
    public GameObject bigSlime;

    public GameObject leftBorder;
    public GameObject rightBorder;

    public GameObject diePos;
    public GameObject bossNPC;

    private GameObject[] slimeBorn;
    private bool talk = true;
    private bool isInRange = false;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        bossNPC.SetActive(false);
        slimeBorn = new GameObject[4];
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        checkSlimeBorn();
    }

    private void checkSlimeBorn()
    {
        bool allDestroyed = true;
        foreach(GameObject obj in slimeBorn)
        {
            if(obj != null)
            {
                allDestroyed = false;
            }
        }
        if (allDestroyed)
        {
            if (count == 4)
            {
                bossNPC.SetActive(true);
                this.gameObject.SetActive(false);
            }
            count = count + 1;
            if (count == 1)
            {
                InitiateSlime(slime, slime, slime, slime);
            } else if (count == 2)
            {
                InitiateSlime(slime, bigSlime, slime, slime);
            } else if (count == 3)
            {
                InitiateSlime(slime, bigSlime, bigSlime, bigSlime);
            }
            else if (count == 4)
            {
                InitiateSlime(bigSlime, bigSlime, bigSlime, bigSlime);
            }
        }
    }

    void InitiateSlime(GameObject slime1, GameObject slime2, GameObject slime3, GameObject slime4)
    {
        GameObject obj1=Instantiate(slime1, initiatePos[0].transform.position, transform.rotation);
        GameObject obj2 = Instantiate(slime2, initiatePos[2].transform.position, transform.rotation);

        GameObject obj3 = Instantiate(slime3, initiatePos[1].transform.position, transform.rotation);
        GameObject obj4 = Instantiate(slime4, initiatePos[3].transform.position, transform.rotation);

        obj1.GetComponent<PlatformEnemy>().leftBorderPos = leftBorder;
        obj1.GetComponent<PlatformEnemy>().rightBorderPos = rightBorder;
        obj2.GetComponent<PlatformEnemy>().leftBorderPos = leftBorder;
        obj2.GetComponent<PlatformEnemy>().rightBorderPos = rightBorder;
        obj3.GetComponent<PlatformEnemy>().leftBorderPos = leftBorder;
        obj3.GetComponent<PlatformEnemy>().rightBorderPos = rightBorder;
        obj4.GetComponent<PlatformEnemy>().leftBorderPos = leftBorder;
        obj4.GetComponent<PlatformEnemy>().rightBorderPos = rightBorder;

        slimeBorn[0] = obj1;
        slimeBorn[1] = obj2;
        slimeBorn[2] = obj3;
        slimeBorn[3] = obj4;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")&&
            other.GetType().ToString()== "UnityEngine.BoxCollider2D")
        {
            isInRange = true;
        }
    }
}
