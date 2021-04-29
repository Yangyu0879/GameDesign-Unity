using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if(target!=null)
        {
            if(transform.position!=target.position)
            {
                Vector2 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                Vector3 tPos= Vector3.Lerp(transform.position, targetPos, smoothing);
                tPos.z = -10;
                transform.position = tPos;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCamPosLimit(Vector2 minPos,Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
