using UnityEngine;
using System.Collections;

public class throwhook : MonoBehaviour {


	public GameObject hook;


	public bool ropeActive;

	GameObject curHook;

    public LayerMask grappleMask;
    public float boostForce = 10000f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	


		if (Input.GetMouseButtonDown (0)) {


			if (ropeActive == false) {
                //Vector2 destiny = Camera.main.ScreenToWorldPoint (Input.mousePosition);


                //curHook = (GameObject)Instantiate (hook, transform.position, Quaternion.identity);

                //curHook.GetComponent<RopeScript> ().destiny = destiny;
                // RaycastHit2D hit = Physics2D.Raycast(destiny, Vector2.zero);
                Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log(destiny - (Vector2)transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, destiny - (Vector2)transform.position, 30, grappleMask); // shoot raycast from player to destiny of length ropeLength
                //Debug.Log(hit.collider != null ? hit.collider.transform.tag : "nothing");
                if (hit.collider != null) //&& hit.collider.transform.tag != "Ungrappable")
                {
                    //AudioManager.instance.Play("HookHit");
                    //curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);
                    Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.Euler(0f, 0f, rotZ -90));
                    curHook.GetComponent<RopeScript>().destiny = hit.point;

                    ropeActive = true;

                }

                //ropeActive = true;
			} else {

				//delete rope

				Destroy (curHook);


				ropeActive = false;

			}
		}
        if (Input.GetMouseButtonDown(1))
        {
            if (ropeActive) // && cc.m_Grounded == false)
            {

                //boostForce += 250f; // slightly higher boostforce when player is grounded

                Debug.Log(boostForce);
                // gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.GetComponent<Rigidbody2D>().velocity * 400); // use this for super boost
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 400f)); // boost player upwards when rope is withdrawn
                //gameObject.GetComponentInChildren<TrailRenderer>().enabled = false; // disable green trial
                //gameObject.transform.Find("Boost Trail").gameObject.SetActive(true);  // enable boost trial

                //Invoke("revertTrail", 0.7f); // change trail renderer back to original
                Destroy(curHook);
                ropeActive = false;
            }

            //delete rope

            



        }
        


    }
    public void resetRope()
    {
        Destroy(curHook);
        ropeActive = false;
    }
}
