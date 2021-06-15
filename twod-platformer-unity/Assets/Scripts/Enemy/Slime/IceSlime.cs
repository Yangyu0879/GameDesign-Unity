using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSlime : Enemy
{
    public GameObject leftBorderPos;
    public GameObject rightBorderPos;
    public GameObject player;
    public GameObject water;
    public GameObject iceHeart;
    public GameObject[] treasureBoxes;

    private bool isFlooding = false;

    public GameObject iceAttack;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        health = 26;
        StartCoroutine(IceAttack());
        StartCoroutine(Flooding());
    }

    // Update is called once per frame
    void Update()
    {
        moveToPlayer();
        DetectAndDestroy();
    }

    protected override void DetectAndDestroy()
    {
        if (health <= 0)
        {
            foreach(GameObject obj in treasureBoxes)
            {
                obj.SetActive(true);
            }
            Destroy(iceHeart.gameObject);
            Destroy(gameObject);
        }
    }

    void moveToPlayer()
    {
        if(player.transform.position.x <= rightBorderPos.transform.position.x &&
            player.transform.position.x >= leftBorderPos.transform.position.x)
        {
            Vector3 dir = new Vector3(player.transform.position.x - this.transform.position.x, 0, 0);
            this.transform.Translate(dir*Time.deltaTime,Space.World);
        }
    }

    public void heartAttack()
    {
        TakeDamage(2);
    }
    IEnumerator IceAttack()
    {
        while (true)
        {
            UnityEngine.Debug.Log("Ice Attack!");
            GameObject obj = Instantiate(iceAttack,transform.position,transform.rotation);
            obj.GetComponent<IceAttack>().dir = player.transform.position - this.transform.position;
            obj.GetComponent<IceAttack>().bot = leftBorderPos.transform.position.y;
            obj.GetComponent<IceAttack>().player = player;
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator Flooding()
    {
        while (true)
        {
            if (isFlooding)
            {
                water.SetActive(false);
                isFlooding = false;
            } else
            {
                water.SetActive(true);
                isFlooding = true;
            }
            yield return new WaitForSeconds(5.0f);
        }
    }
}
