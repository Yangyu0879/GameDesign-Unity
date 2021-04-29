using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed = 2;
    public float startWaitTime = 1.0f;
    //wait when arrive for sometime
    private float waitTime;

    public Transform movePos;
    public Transform leftDownPos;
    public Transform rightUpPos;
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        waitTime = startWaitTime;
        //random init startPos
        movePos.position = GetRandomPosition();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        //move
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);
        //if arrive
        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)
        {
            //wait
            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                movePos.position = GetRandomPosition();
                waitTime = startWaitTime;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        return new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x),
            Random.Range(leftDownPos.position.y, rightUpPos.position.y));
    }
}
