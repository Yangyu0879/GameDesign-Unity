using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;

    //绳索发出与否
	public bool ropeActive;

	public GameObject curHook;

    public float ropeLength = 6f;

    public LayerMask grappleMask;

    public float launchForce = 1000f;

    public float offset=-90;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        launchForce = 1000f;

        //发绳索
		if (Input.GetMouseButtonDown (0)) {
            //鼠标点击点为目的地
            Vector2 dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (ropeActive == false) {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dest - (Vector2)transform.position, ropeLength, grappleMask); // shoot raycast from player to destiny of length ropeLength
                //Debug.Log(hit.collider != null ? hit.collider.transform.tag : "nothing");
                if (hit.collider != null&& hit.collider.transform.tag != "Ungrappable")//某些场景Tag设置为不可抓取
                {
                    //AudioManager.instance.Play("HookHit");
                    //curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.Euler(0f, 0f, rotZ+offset));
                    curHook.GetComponent<RopeScript>().dest = hit.point;

                    ropeActive = true;

                }
			} 
		}

        //收绳索
        if (Input.GetMouseButtonDown(1))
        {
            if (ropeActive) // && cc.m_Grounded == false)
            {
                //if(!canJump) launchForce += 250f; // slightly higher boostforce when player is grounded

                // gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * 400); // use this for super boost
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, launchForce)); // boost player upwards when rope is withdrawn
                gameObject.transform.Find("Trail").gameObject.SetActive(false);  // disable green trial
                gameObject.transform.Find("launchTrail").gameObject.SetActive(true);  // enable boost trial

                Invoke("recoveryTrail", 0.7f); // change trail renderer back to original
            }
            resetRope();
        }

        if(Input.GetButtonDown("Jump"))
        {
            if(ropeActive)
            {
                resetRope();
            }
        }

    }

    void recoveryTrail()
    {
        gameObject.transform.Find("Trail").gameObject.SetActive(true);  // disable green trial
        gameObject.transform.Find("launchTrail").gameObject.SetActive(false);  // enable boost trial
    }

    public void resetRope()
    {
        Destroy(curHook);
        ropeActive = false;
    }
}
