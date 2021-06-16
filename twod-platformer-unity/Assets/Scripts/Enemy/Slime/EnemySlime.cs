using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemySlime : Enemy
{
    public float speed = 1.0f;
    public float detectRadius = 2.0f;
    public Tilemap tilemap;
    protected enum EnemyState
    {
        STAND,      //原地呼吸
        CHECK,      //原地观察
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
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();
        previousLoc = new UnityEngine.Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        IdentifyState();
        StateMachine();
        DetectBorderTileExisted();
    }

    protected virtual void IdentifyState(){
        if(PlayerInRange() && currentState==EnemyState.WALK){
            StartCoroutine(Warning());
            SetAnimPath("WALK_TO_WARN");
            currentState=EnemyState.WARN;
        } else if(!PlayerInRange() && currentState==EnemyState.CHASE){
            //UnityEngine.Debug.Log("CHASE TO WALK");
            currentState=EnemyState.CHECK;
            StartCoroutine(Checking());
        }
    }
    protected virtual void StateMachine(){
        switch(currentState){
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
    protected virtual void Wander(){
        UnityEngine.Vector3 pos1 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3 pos2 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3Int deltaY = new UnityEngine.Vector3Int(0,-1,0);
        UnityEngine.Vector3Int leftdown_cell = tilemap.WorldToCell(pos1) + deltaY;
        UnityEngine.Vector3Int rightdown_cell = tilemap.WorldToCell(pos2) + deltaY;
        //tilemap.SetTile(pos1cell,null);
        bool left_border_tile_existed = tilemap.HasTile(leftdown_cell);
        bool right_border_tile_existed = tilemap.HasTile(rightdown_cell);

        if(dir == 1.0f && (right_border_tile_existed == false || Math.Abs(enemyRigidBody.velocity.x)<0.1)){
            dir = - 1.0f;
            //UnityEngine.Debug.Log("Dir Change");
        } else if(dir == - 1.0f && (left_border_tile_existed==false || Math.Abs(enemyRigidBody.velocity.x)<0.1)){
            dir = 1.0f;
            //UnityEngine.Debug.Log("Dir Change");
        }
        
        //UnityEngine.Debug.Log("Platform Wandering");
        enemyRigidBody.velocity = new UnityEngine.Vector2(speed*dir,enemyRigidBody.velocity.y);
        previousLoc=transform.position;
        //UnityEngine.Debug.Log("delta:"+System.Math.Abs(enemyRightBoarder - rightBoarder).ToString());        
    }

    protected virtual void Chase(){
        float dirToPlayer = transform.position.x - player.transform.position.x>0 ? -1:1;
        enemyRigidBody.velocity = new UnityEngine.Vector2(2*speed*dirToPlayer,enemyRigidBody.velocity.y);
    }

    protected void OnTriggerEnter2D(Collider2D collider){
        base.OnTriggerEnter2D(collider);

        //UnityEngine.Debug.Log("Platform Change");
        if(collider.transform.tag=="Enemy"){
            return;
        }
        if(tilemap==null){
            UnityEngine.Debug.Log("tilemap is null");
            return ;
        }
        UnityEngine.Vector3 pos1 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3 pos2 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3Int pos1cell = tilemap.WorldToCell(pos1);
        UnityEngine.Vector3Int pos2cell = tilemap.WorldToCell(pos2);
        
        enemyLeftBoarder = enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x/2;
        enemyRightBoarder = enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x/2;
    }

    protected void DetectBorderTileExisted(){
        UnityEngine.Vector3 pos1 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x - enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3 pos2 = new UnityEngine.Vector3(enemyBoxCollider.transform.position.x + enemyBoxCollider.size.x/2, enemyBoxCollider.transform.position.y - enemyBoxCollider.size.y/2, 0);
        UnityEngine.Vector3Int deltaY = new UnityEngine.Vector3Int(0,-1,0);
        UnityEngine.Vector3Int leftdown_cell = tilemap.WorldToCell(pos1) + deltaY;
        UnityEngine.Vector3Int rightdown_cell = tilemap.WorldToCell(pos2) + deltaY;
        //tilemap.SetTile(pos1cell,null);
        bool left_border_tile_existed = tilemap.HasTile(leftdown_cell);
        bool right_border_tile_existed = tilemap.HasTile(rightdown_cell);
        UnityEngine.Debug.Log("Left Border Tile Existed:"+left_border_tile_existed.ToString());
        UnityEngine.Debug.Log("Right Border Tile Existed:"+right_border_tile_existed.ToString());
    }

    protected bool PlayerInRange(){
        if(player == null){
            return false;
        }
        float distanceToPlayer = UnityEngine.Vector2.Distance(player.transform.position,transform.position);
        return distanceToPlayer<detectRadius;
    }

    protected virtual IEnumerator Warning() {
        yield return new WaitForSeconds(2.0f);
        if(PlayerInRange()){
            currentState=EnemyState.CHASE;
            //UnityEngine.Debug.Log("WARN TO CHASE");
            SetAnimPath("WARN_TO_CHASE");
        } else {
            currentState=EnemyState.WALK;
            //UnityEngine.Debug.Log("WARN TO WALK");
            SetAnimPath("WARN_TO_WALK");
        }
    }

    protected virtual IEnumerator Checking() {
        yield return new WaitForSeconds(2.0f);
        if(PlayerInRange()){
            currentState=EnemyState.CHASE;
            //UnityEngine.Debug.Log("CHECK TO CHASE");
        } else {
            currentState=EnemyState.WALK;
            //UnityEngine.Debug.Log("CHECK TO WALK");
            SetAnimPath("CHASE_TO_WALK");
        }
    }
    
    protected void SetAnimPath(string state){
        enemyAnim.SetBool("WALK_TO_WARN",state == "WALK_TO_WARN"? true:false);
        enemyAnim.SetBool("WARN_TO_WALK",state == "WARN_TO_WALK"? true:false);
        enemyAnim.SetBool("WARN_TO_CHASE",state == "WARN_TO_CHASE"? true:false);
        enemyAnim.SetBool("CHASE_TO_WALK",state == "CHASE_TO_WALK"? true:false);
    }
}
