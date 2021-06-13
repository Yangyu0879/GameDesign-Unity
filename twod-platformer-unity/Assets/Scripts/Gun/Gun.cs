using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzleTransform;
    //public Camera cam;
    private Vector3 mousePos;
    private Vector2 gunDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gunDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if (Input.GetMouseButtonDown(2))
        {
            Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));

        }
    }
}
