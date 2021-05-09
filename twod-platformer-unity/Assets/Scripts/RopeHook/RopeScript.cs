using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class RopeScript : MonoBehaviour {

	public Vector2 dest;

	public float speed= 1;

	public float distance=2;

	public GameObject nodePrefab;

	public GameObject player;

	public GameObject lastNode;

	public int damage = 1;

	public LineRenderer lr;

	int vertexCount=2;

	public List<GameObject> Nodes = new List<GameObject>();

    public GameObject sparksEffect;

    public GameObject hitEnemySparksEffect;

    public float hookPushForce = 1000f;
    public float hookPullForce = 1500f;

    bool done=false;

	// Use this for initialization
	void Start () {
	

		lr = GetComponent<LineRenderer> ();

		player = GameObject.FindGameObjectWithTag ("Player");

		lastNode = transform.gameObject;
		
		Nodes.Add (transform.gameObject);


	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log("transform.position:"+transform.position);
		Debug.Log("dest" + dest);
		transform.position = Vector2.MoveTowards (transform.position,dest,speed);

		if ((Vector2)transform.position != dest) {
			Vector3 vector3 = new Vector3(0f, 0.6f, 0f);

			if (Vector2.Distance (player.transform.position-vector3, lastNode.transform.position) > distance) {
				//

				CreateNode ();
			}


		} else if (done == false) {

			done = true;
			Vector3 vector3 = new Vector3(0f, 0.6f, 0f);

			while (Vector2.Distance (player.transform.position - vector3, lastNode.transform.position) > distance)
			{
				CreateNode ();
			}


			lastNode.GetComponent<HingeJoint2D> ().connectedBody = player.GetComponent<Rigidbody2D> ();
		}


		RenderLine ();
	}


	void RenderLine()
	{

		//lr.SetVertexCount (vertexCount);
        lr.positionCount = vertexCount;


        int i;
		for (i = 0; i < Nodes.Count; i++) {

			lr.SetPosition (i, Nodes [i].transform.position);

		}

		Vector3 vector3 = new Vector3(0f, 0.6f, 0f);
		lr.SetPosition (i, player.transform.position- vector3);

	}


	void CreateNode()
	{
		Vector3 vector3 = new Vector3(0f, 0.6f, 0f);

		Vector2 pos2Create = player.transform.position- vector3 - lastNode.transform.position;
		pos2Create.Normalize ();
		pos2Create *= distance;
		pos2Create += (Vector2)lastNode.transform.position;

		GameObject go = (GameObject)	Instantiate (nodePrefab, pos2Create, Quaternion.identity);

		go.tag = "Grappling Hook";

		go.transform.SetParent (transform);

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = go.GetComponent<Rigidbody2D> ();

		lastNode = go;

		Nodes.Add (lastNode);

		vertexCount++;

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		Enemy enemy = collision.GetComponent<Enemy>();
		Debug.Log("hook enemy collision");
		if(enemy!=null)
        {
			enemy.TakeDamage(damage);
			Instantiate(hitEnemySparksEffect, transform.position, transform.rotation);
			Invoke("resetRope", 0.2f);
			Vector2 dirOfObj = (Vector2)transform.position - (Vector2)collision.GetComponent<Transform>().position;
			collision.GetComponent<Rigidbody2D>().AddForce(dirOfObj.normalized * -enemy.knockbackFromWep);
        }
		Instantiate(sparksEffect, transform.position, transform.rotation);
    }

    void resetRope()
    {
		Destroy(player.GetComponent<throwhook>().curHook);
		player.GetComponent<throwhook>().ropeActive = false;
    }
}
