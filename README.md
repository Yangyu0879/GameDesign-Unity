# ToDo

1. 完善场景
+ 增加背景图片
+ 将已有的场景精细化，比如调整平台高度到合适，增加更多元素以形成一个完整的关卡
2. 增加元素，比如金币、陷阱等
3. 调整人物钩锁、冲刺残影这些动作，使其更流畅精美。
4. 增加主菜单界面和其它来形成一个完整的UI
+ 玩家死亡后是重生还是回到主菜单
+ 主菜单、设置、游戏、关卡选择等逻辑
5. 增加敌人种类
6. 增加主角技能

# 4.29 更新

1.文件结构调整

Assets
+ Animations 角色、UI各种动画和动画控制器Animator
+ PhysicsMaterials 碰撞框的物理材质，比如摩擦力弹力这些
+ Prefabs 预制模板，目前有敌人、玩家等，建议将所有需要复用的场景GameObj都做一个模板
+ Scenes 关卡场景文件
+ Scripts C#脚本文件
	+ Camera 控制相机运动、动画的脚本
	+ Enemy 控制敌人属性、行为的脚本，已经实现了基类Enemy，其它类必须继承基类。
	+ Player 玩家操作、属性、行为的脚本，基本上将其拖入Player这一GameObj下
	+ RopeHook 实现钩锁功能的脚本，之后如果有其它工具，也可以将其改名为Tool
	+ UI 控制UI显示的脚本
+ Sprites 所有2D素材，比如场景、玩家敌人动画、瓦片地图等。
有些素材没有分入大类比较凌乱，其创作者可以自己将其归入以下大类中。
	+ Enemy 敌人动画
	+ Player 玩家动画
	+ UI 
	+ Ground 场景
	+ Materials 小材料，一些场景物件
	+ Palettes 这是瓦片地图专用文件夹，熟悉瓦片地图的人操作。

有些文件我没有归类，请创作者自己将其归类

2.增加了相机跟随玩家移动的功能
将CameraFollow脚本附加在MainCamera下并将target绑定玩家
注意：对于每个关卡，都要预先设定相机的左下，右上边界以免越界
2D画面的Z轴图层为-10，不能改变

3.解决了玩家会粘在墙上的问题

4.增加了ReadMe文档，之后更新可以汇总在这里

# 4.29 更新

1.增加了玩家的生命-伤害系统和敌人的生命-伤害系统
（1）调用PlayerHealth的DamagePlayer(damage)函数就可以伤害玩家，
在Enemy基类中已经写好了“敌人碰到玩家就会给予伤害的”功能，
如果后续还要增加“敌人攻击玩家给与伤害”，“陷阱伤害”部分，就需要调用函数
还可以调用RecoverPlayer(recovery)来回复玩家
（2）调用Enemy基类的TakeDamage(damageTaken)就可以伤害敌人，
这一点在Player类中已经写好
（3）增加了玩家受伤、死亡动画，注意死亡后的处理部分还没有完成

附调用方法：

（1）玩家Player受到伤害或回复
```
playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"&& collision.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }
        }
    }
```
（2）敌人Enemy受到伤害
```
void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
```
2.增加了血条UI

3.为了实验，完善了一个最简单的蝙蝠UI，并且已经放入prefab作为模板，
需要时直接从prefab中拉进场景就行，拉进后注意设置以下两个属性
（1）设定Bat飞行范围（矩形）的左下角LeftDownPos
（2）设定Bat飞行范围（矩形）的右上角RightUpPos


