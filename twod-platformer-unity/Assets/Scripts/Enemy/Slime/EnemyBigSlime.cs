using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBigSlime : PlatformEnemy
{
    // Start is called before the first frame update
    public GameObject slime;
    public float splitSpeed = 3.0f;
    public float splitUpSpeed = 3.0f;
    void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();
        previousLoc = new UnityEngine.Vector3(0, 0, 0);
        slime.tag = "Enemy";
        slime.GetComponent<PlatformEnemy>().rightBorderPos = rightBorderPos;
        slime.GetComponent<PlatformEnemy>().leftBorderPos = leftBorderPos;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        IdentifyState();
        StateMachine();
    }

    protected override void DetectAndDestroy(){
        if(health <= 0){
            enemyRigidBody.velocity = new UnityEngine.Vector2(0,0);
            CircleCollider2D slimeCircleCollider = slime.GetComponent<CircleCollider2D>();
            float radius = slimeCircleCollider.radius;
            Vector3 pos = transform.position;
            Vector3 deltax = new Vector3(2.5f*radius,0,0);
            Vector3 deltay = new Vector3(0,radius,0);
            GameObject obj1 = Instantiate(slime, pos-deltax+deltay, transform.rotation);
            GameObject obj2 = Instantiate(slime, pos+deltay, transform.rotation);
            GameObject obj3 = Instantiate(slime, pos+deltax+deltay, transform.rotation);

            Rigidbody2D obj1RigidBody = obj1.GetComponent<Rigidbody2D>();
            obj1RigidBody.velocity = new UnityEngine.Vector2(-1*splitSpeed,splitUpSpeed);
            Rigidbody2D obj2RigidBody = obj2.GetComponent<Rigidbody2D>();
            obj2RigidBody.velocity = new UnityEngine.Vector2(0,splitUpSpeed);
            Rigidbody2D obj3RigidBody = obj3.GetComponent<Rigidbody2D>();
            obj3RigidBody.velocity = new UnityEngine.Vector2(splitSpeed,splitUpSpeed);
            Destroy(gameObject);
        }
    }
}
