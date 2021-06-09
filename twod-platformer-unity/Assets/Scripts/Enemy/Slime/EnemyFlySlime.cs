using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlySlime : EnemySlime
{
    public Transform posStart;
    public Transform posEnd;
    private UnityEngine.Vector3 posCenter;
    private UnityEngine.Vector3 moveDir;
    private UnityEngine.Vector3 dirToStart,dirToEnd;
    private bool previousIsReturn = false;
    
    void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
        enemyRigidBody = GetComponent<Rigidbody2D>();
        enemyBoxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();
        previousLoc = new UnityEngine.Vector3(0,0,0);

        dirToStart= (posStart.position - posEnd.position).normalized;
        dirToEnd = -dirToStart;
        moveDir = dirToStart;
        transform.position = posStart.position;
        posCenter = (posStart.position + posEnd.position)*0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        
        IdentifyState();
        StateMachine();
    }

    protected override void StateMachine(){
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
                ReturnToRoutine();
                break;
        }
    }

    protected override void IdentifyState(){
        if(PlayerInRange() && (currentState==EnemyState.WALK||currentState==EnemyState.RETURN)){
            stopMovement();
            if(currentState==EnemyState.RETURN){
                previousIsReturn = true;
            }
            StartCoroutine(Warning());
            SetAnimPath("WALK_TO_WARN");
            currentState=EnemyState.WARN;
        } else if(!PlayerInRange() && currentState==EnemyState.CHASE){
            stopMovement();
            StartCoroutine(Checking());
            //UnityEngine.Debug.Log("CHASE TO RETURN");
            currentState=EnemyState.CHECK;
        }
    }

    protected override void Wander(){
        if(moveDir == dirToStart && UnityEngine.Vector3.Distance(posStart.position, transform.position) < 0.01f){
            moveDir = dirToEnd;
            //UnityEngine.Debug.Log("Dir Change");
        } else if(moveDir == dirToEnd && UnityEngine.Vector3.Distance(posEnd.position, transform.position) < 0.01f){
            moveDir = dirToStart;
            //UnityEngine.Debug.Log("Dir Change");
        }
        
        //UnityEngine.Debug.Log("Fly Wandering");
        
        enemyRigidBody.velocity = moveDir*speed;
        //UnityEngine.Debug.Log(enemyRigidBody.velocity);
    }
    
    protected override void Chase(){
        UnityEngine.Vector3 playerPos = player.transform.position;
        UnityEngine.Vector3 chaseDir = (player.transform.position-transform.position).normalized;

        enemyRigidBody.velocity = 2*chaseDir*speed;
    }

    protected override IEnumerator Checking() {
        yield return new WaitForSeconds(2.0f);
        if(PlayerInRange()){
            currentState=EnemyState.CHASE;
            //UnityEngine.Debug.Log("CHECK TO CHASE");
        } else {
            currentState=EnemyState.RETURN;
            //UnityEngine.Debug.Log("CHECK TO RETURN");
            SetAnimPath("CHASE_TO_WALK");
        }
    }
    protected override IEnumerator Warning() {
        yield return new WaitForSeconds(2.0f);
        if(PlayerInRange()){
            currentState=EnemyState.CHASE;
            //UnityEngine.Debug.Log("WARN TO CHASE");
            SetAnimPath("WARN_TO_CHASE");
        } else {
            if(previousIsReturn){
                currentState=EnemyState.RETURN;
            }else {
                currentState=EnemyState.WALK;
            }
            previousIsReturn=false;
            //UnityEngine.Debug.Log("WARN TO WALK");
            SetAnimPath("WARN_TO_WALK");
        }
    }

    void stopMovement(){
        enemyRigidBody.velocity = new UnityEngine.Vector2(0.0f,0.0f);
    }
    void ReturnToRoutine(){
        UnityEngine.Vector3 returnDir = (posCenter-transform.position).normalized;
        enemyRigidBody.velocity = returnDir*speed;
        if(UnityEngine.Vector3.Distance(posCenter, transform.position) < 0.01f){
            currentState=EnemyState.WALK;
        }
    }
}
