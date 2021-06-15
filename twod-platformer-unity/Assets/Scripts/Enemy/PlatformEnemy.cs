using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformEnemy : Enemy
{
    public float speed = 1.0f;
    public float detectRadius = 5.0f;
    protected Tilemap tilemap;
    protected enum EnemyState
    {
        STAND,      //原地呼吸
        CHECK,       //原地观察
        WALK,       //移动
        WARN,       //盯着玩家
        CHASE,      //追击玩家
        RETURN      //超出追击范围后返回
    }
    protected EnemyState currentState = EnemyState.WALK;

    protected float leftBoarder = 0.0f;
    protected float rightBoarder = 0.0f;
    protected float enemyLeftBoarder = 0.0f;
    protected float enemyRightBoarder = 0.0f;
    protected UnityEngine.Vector3 previousLoc;
    protected float dir = 1.0f;

    protected Rigidbody2D enemyRigidBody;
    protected BoxCollider2D enemyBoxCollider;
    protected GameObject player;
    protected Animator enemyAnim;

    public GameObject leftBorderPos;
    public GameObject rightBorderPos;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();
        previousLoc = new UnityEngine.Vector3(0, 0, 0);

        tilemap = GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>();
        if (GameObject.FindGameObjectWithTag("Ground") == null)
        {
            UnityEngine.Debug.Log("Tilemap Ground Object Not Found");
        }
        if (GameObject.FindGameObjectWithTag("Ground").GetComponent<Tilemap>()==null)
        {
            UnityEngine.Debug.Log("Tilemap Component Not Found");
        }
        Debug.Log("Tilemap:"+tilemap.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        IdentifyState();
        StateMachine();
    }

    protected virtual void IdentifyState()
    {
        if (PlayerInRange() && currentState == EnemyState.WALK)
        {
            StartCoroutine(Warning());
            SetAnimPath("WALK_TO_WARN");
            currentState = EnemyState.WARN;
        }
        else if (!PlayerInRange() && currentState == EnemyState.CHASE)
        {
            //UnityEngine.Debug.Log("CHASE TO WALK");
            currentState = EnemyState.CHECK;
            StartCoroutine(Checking());
        }
    }
    protected virtual void StateMachine()
    {
        switch (currentState)
        {
            case EnemyState.STAND:
                break;
            case EnemyState.CHECK:
                break;
            case EnemyState.WALK:
                Wander();
                break;
            case EnemyState.WARN:
                break;
            case EnemyState.CHASE:
                Chase();
                break;
            case EnemyState.RETURN:
                break;
        }
    }
    protected void Wander()
    {
        UnityEngine.Vector3 pos1 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x / 2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y / 2, 0);
        UnityEngine.Vector3 pos2 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x / 2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y / 2, 0);

        if (dir == 1.0f && (pos2.x > rightBorderPos.transform.position.x||Math.Abs(enemyRigidBody.velocity.x)<0.1))
        {
            dir = -1.0f;
            UnityEngine.Debug.Log("Enemy Dir Change:"+pos1.x.ToString()+" larger "+rightBoarder.ToString());
        }
        else if (dir == -1.0f && (pos1.x < leftBorderPos.transform.position.x || Math.Abs(enemyRigidBody.velocity.x) < 0.1))
        {
            dir = 1.0f;
            UnityEngine.Debug.Log("Enemy Dir Change" + pos2.x.ToString() + " smaller " + leftBoarder.ToString());
        }

        //UnityEngine.Debug.Log("Platform Wandering");
        enemyRigidBody.velocity = new UnityEngine.Vector2(speed * dir, enemyRigidBody.velocity.y);
        previousLoc = transform.position;
        //UnityEngine.Debug.Log("delta:"+System.Math.Abs(enemyRightBoarder - rightBoarder).ToString());        
    }

    protected void Chase()
    {
        float dirToPlayer = transform.position.x - player.transform.position.x > 0 ? -1 : 1;
        if(enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x / 2 < leftBorderPos.transform.position.x
            || enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x / 2 > rightBorderPos.transform.position.x)
        {
            enemyRigidBody.velocity = new UnityEngine.Vector2(0, enemyRigidBody.velocity.y);
            return;
        }
        if ((dirToPlayer == 1 && enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x * 0.5f >= player.transform.position.x - player.GetComponent<BoxCollider2D>().size.x * 0.5f)
            || (dirToPlayer == -1 && enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x * 0.5f <= player.transform.position.x + player.GetComponent<BoxCollider2D>().size.x * 0.5f))
        {
            enemyRigidBody.velocity = new UnityEngine.Vector2(0, enemyRigidBody.velocity.y);
            return;
        }
        enemyRigidBody.velocity = new UnityEngine.Vector2(2 * speed * dirToPlayer, enemyRigidBody.velocity.y);
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        UnityEngine.Debug.Log("Collider Tag:" + collider.tag);
        //UnityEngine.Debug.Log("Platform Change");
        if (collider.transform.tag == "Enemy")
        {
            return;
        }
    }

    protected bool PlayerInRange()
    {
        if (player == null)
        {
            return false;
        }
        float distanceToPlayer = UnityEngine.Vector2.Distance(player.transform.position, transform.position);
        return distanceToPlayer < detectRadius;
    }

    protected IEnumerator Warning()
    {
        yield return new WaitForSeconds(2.0f);
        if (PlayerInRange())
        {
            currentState = EnemyState.CHASE;
            //UnityEngine.Debug.Log("WARN TO CHASE");
            SetAnimPath("WARN_TO_CHASE");
        }
        else
        {
            currentState = EnemyState.WALK;
            //UnityEngine.Debug.Log("WARN TO WALK");
            SetAnimPath("WARN_TO_WALK");
        }
    }

    protected IEnumerator Checking()
    {
        yield return new WaitForSeconds(2.0f);
        if (PlayerInRange())
        {
            currentState = EnemyState.CHASE;
            //UnityEngine.Debug.Log("CHECK TO CHASE");
        }
        else
        {
            currentState = EnemyState.WALK;
            //UnityEngine.Debug.Log("CHECK TO WALK");
            SetAnimPath("CHASE_TO_WALK");
        }
    }

    protected void SetAnimPath(string state)
    {
        enemyAnim.SetBool("WALK_TO_WARN", state == "WALK_TO_WARN" ? true : false);
        enemyAnim.SetBool("WARN_TO_WALK", state == "WARN_TO_WALK" ? true : false);
        enemyAnim.SetBool("WARN_TO_CHASE", state == "WARN_TO_CHASE" ? true : false);
        enemyAnim.SetBool("CHASE_TO_WALK", state == "CHASE_TO_WALK" ? true : false);
    }
}
