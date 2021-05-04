﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float doulbJumpSpeed;

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    private BoxCollider2D myFeet;
    private bool isGround;
    private bool canDoubleJump;
    public float forcetoAdd = 100;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;
    [Header("Dash参数")]
    public Vector2 dashVector; 
    public float dashtime;//dash
    private float dashtimeleft;
    private float lastDash;
    public float dashspeed;
    public float dashCoolDown;

    public bool isDashing;

    bool jumpPressed;
    int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        Run();
        if (Input.GetButtonDown("Fire1"))
        {
            ReadyTodash();
        }
        if (isDashing)
        {
            isDashing = false;
            return;
        }
        Dash();
        Jump();
        //Attack();
        CheckGrounded();
        SwitchAnimation();
        //Dash();


    }

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }

    void Flip()
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if(plyerHasXAxisSpeed)
        {
            if(myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool plyerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", plyerHasXAxisSpeed);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(isGround)
            {
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else
            {
                if(canDoubleJump)
                {
                    myAnim.SetBool("DoubleJump", true);
                    Vector2 doubleJumpVel = new Vector2(0.0f, doulbJumpSpeed);
                    myRigidbody.velocity = Vector2.up * doubleJumpVel;
                    canDoubleJump = false;
                }
            }
        }
    }

    //void Attack()
    //{
    //    if(Input.GetButtonDown("Attack"))
    //    {
    //        myAnim.SetTrigger("Attack");
    //    }
    //}

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (myAnim.GetBool("Jump"))
        {
            if(myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if(isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void ReadyTodash()
    {
        //Debug.Log("dfighoirej");
        dashVector = new Vector2(dashspeed * myRigidbody.velocity.x, dashspeed * myRigidbody.velocity.y);
        isDashing = true;
        dashtimeleft = dashtime;
        lastDash = Time.time;

    }
    void Dash()
    {
        //isDashing = true;
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    Debug.Log("ddknhfhrejf");
            //dashtimeleft -= Time.deltaTime;
            //ShadowPool.instance.GetFormPool();
            if (dashtimeleft > 0)
            {
                myRigidbody.velocity = dashVector;
                dashtimeleft -=Time.deltaTime;
                ShadowPool.instance.GetFormPool();
            }

        //}
        if (dashtimeleft <= 0)
        {
            isDashing = false;
        }
    }
}
