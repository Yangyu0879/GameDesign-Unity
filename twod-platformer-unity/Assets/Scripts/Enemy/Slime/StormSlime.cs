using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormSlime : MonoBehaviour
{
    // Start is called before the first frame update
    protected enum BossState
    {
        STAND,      //原地呼吸
        CHECK,       //原地观察
        WALK,       //移动
        WARN,       //盯着玩家
        CHASE      //追击玩家
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
