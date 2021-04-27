using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	public float forcetoAdd=100;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    private float horizontalMove;
    public Transform groundCheck;
    public LayerMask ground;
    [Header("Dash参数")]
    public float dashtime;//dash
    private float dashtimeleft;
    private float lastDash;
    public float dashspeed;
    public float dashCoolDown;

    public bool isGround, isJump, isDashing;

    bool jumpPressed;
    int jumpCount;

    void Start () {
		//gives it force
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * 10;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }


	void Update () {
        Debug.Log("didkdkdl");
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

        Dash();
        if (isDashing)
        {
            return;
        }
        if (Input.GetKey (KeyCode.A))  
			GetComponent<Rigidbody2D> ().AddForce(-Vector2.right*forcetoAdd);

		if (Input.GetKey (KeyCode.D))  
			GetComponent<Rigidbody2D> ().AddForce(Vector2.right*forcetoAdd);

        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {
                ReadyTodash();
            }
        }
    }
    //private void FixedUpdate()
    //{
    //    isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);

    //    Dash();
    //    if (isDashing)
    //    {
    //        return;
    //    }
    //}
    void ReadyTodash()
    {

        isDashing = true;
        dashtimeleft = dashtime;
        lastDash = Time.time;

    }
    void Dash()
    {
        Debug.Log(isDashing);
        if (isDashing)
        {
            if (dashtimeleft > 0)
            {
                rb.velocity = new Vector2(dashspeed * horizontalMove, rb.velocity.y);
                dashtimeleft -= Time.deltaTime;
                ShadowPool.instance.GetFormPool();
            }

        }
        if (dashtimeleft <= 0)
        {
            isDashing = false;
        }
    }
}
