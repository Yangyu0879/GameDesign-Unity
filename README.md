# ToDo

1. 完善场景

- 将已有的场景精细化，比如调整平台高度到合适，增加更多元素以形成一个完整的关卡
- 考虑设计多个地图来完成 roguelike 的形式

2. 增加元素，比如金币、陷阱等,文件夹命名为 Item
3. 调整人物钩锁、冲刺残影这些动作，使其更流畅精美。
4. 增加主菜单界面和其它来形成一个完整的 UI

- 玩家死亡后是重生还是回到主菜单
- 主菜单、设置、游戏、关卡选择等逻辑

5. 增加敌人种类
6. 增加主角技能

- ToDo List

  - 人物
    - 改善绳索的表现。
    - 对人物 Sprite 的尺寸、动作、物理等进行调试以达到较好效果。
    - 完善人物操作控制，如果有需要，加入一些按键。
    - 完善人物与场景、敌人的接口：目前已完成：
      - 玩家受伤 Player/playerHealth.DamagePlayer(d)
      - 玩家回血 Player/playerHealth.RecoverPlayer(r)
      - 玩家得分 UI/GameScene/ScoreBoard.AddScore(s)
      - 敌人受伤 Enemy/Enemy.TakeDamage(d)，让所有敌人继承 Enemy 类
    - 如果完成以上还有空余精力，可以试着加入更多元素：大招、魔法、宠物、更多道具、更多能力、更多属性等等。
  - 敌人
    - 加入更多敌人种类。放在文件夹 Enemy 下。
    - 敌人与玩家、场景互动。
    - To Be Filled
  - 场景

    - 完善场景 Level 1 到 3。
    - 加入更多场景元素，使可玩性更高或者视觉感受更好。放在文件夹 Item 下。
    - To Be Filled

# 5.13 更新 -hyo

1.增加玩家受伤屏幕红色闪动。

2.实现了玩家受伤相机抖动，但是好像屏幕还是固定的。原因不明。

3.实现玩家和敌人受伤血液粒子特效。
在Prefab/ParticleEffect下可以修改玩家敌人的掉血特效。
对于每个新建的敌人模板，需要把预制体文件拖到hitSparkEffect属性上。

# 5.11 更新 -mjx

1. 加入宝箱(H)与传送门(B)

2. 修改金币 prefab 为有重力型(Coin prefab)和无重力型(NoGravityCoin prefab)

3. 加入 TriggerBox 层，该层只与 Player 碰撞，用以触发人物碰撞，具体在 ProjectSetting 中 Physics2D 查看

4.修改 gitignore

# 5.9 更新 -mjx

1.加入金币 Brush

2.优化场景

##### 3.**加入多个其他模板素材，具体可在 OthersMaterial 文件夹下查看，按需使用，以后其他模板素材可放在该文件夹下，勿删**

4.加入熔岩、指示牌、鬼火灯等 tile

# 5.9 更新 -hyo

1.为玩家加入 IsDead 字段，外部只能获取不能修改。

2.调整了死亡动画播放时间。

# 5.9 更新 -hyo

1.为 Player 加入 PolygonCollider，主要负责玩家的伤害判定。
敌人和陷阱会检测这个碰撞框是否与之碰撞。
在玩家受伤后这个框会禁用一段时间，这段时间人物是无敌模式，不接受伤害。

2.修改了 PlayerPrefab 中攻击碰撞框的位置问题。

3.在暂停时禁用绳子。

4.ToDo：

- 在主菜单和游戏场景切换时加入动画使其更加顺滑。
- 加入受伤粒子特效，增强交互感。
- 在人物碰到敌人和陷阱时最好给一个反推力，这样更真实。
- 如果有时间可以在钩锁外加入更多道具，比如回力镖、炸弹等。

# 5.9 更新 -cys

![image-20210509204932191](C:\Users\YSChen\AppData\Roaming\Typora\typora-user-images\image-20210509204932191.png)

1. 给绳索添加了 1 层，在**Edit -> Project Settings -> Physics**让它不与 player 发生碰撞，防止绳子的撞击让玩家漂移的现象，防止绳子乱飘的现象
2. 设置收绳索时固定获得 10 的速度

# 5.8 更新 -mjx

1.Level1-2 完成 80%左右，**地刺部分需要负责人物的同学再对人物建立一个多边形碰撞来实现地刺持续伤害**

2.加入地刺 tilemap

3.加入移动平台

4.地图内容进行部分文件夹整合

# 5.8 更新 -hyo

1.完善了 Scores 菜单，现在可以显示前五高分。

2.添加了 ScoreBoard.SaveScore()，如果有需要存储分数时请调用。

3.关于 UI 屏幕大小：请将屏幕调整到 16：9 并且关闭 Low Sync。

4.删除了 SampleScene，目前的场景有 MainMenu（编号 0），GameScene（编号 1）和 Level1-2（编号 2）

使用 SceneManager.LoadScene(2);加载场景

# 5.7 更新

mjx:加入地图部分元素，如流水、地刺等，加入新场景 level1-2（未完成），加入冰原地图，加入 RuleTile

cys:设置了绳子初始位置的偏移量，改善绳索的质感

# 5.6 更新 -mjx

1.增加背景图片

# 5.5 更新 -hyo

1.增加了场景

- 主菜单场景 MainMenu， 目前只有开始、选关和退出，之后计划在右上角做设置和分数按钮。
  待完成：settings、scores、selectlevels

  2.增加了游戏中暂停按钮和暂停界面。

  3.增加了死亡界面，可以选择重来、回到主菜单、退出。

- 为 playerHealth 增加了 restartPos 重生地点，默认是开始时的出生点，之后可以加入重置出生点功能。

  4.PlayerScript 脚本中有一些问题，注意修复。

同时鼠标左键同时是放钩锁和冲刺的功能也需要修复。
可能需要对主要是钩锁的操控方式进行一次修改重构。

最好将 PlayerScript 和 PlayerController 脚本合并，如果有一些独立功能再单独分出来。

5.添加了碰撞层 Layers 和显示层 Sorting Layers
主要分出来 Player、Enemy、Item、Ground（有碰撞框可以与玩家互动的地形）、BackGround（纯背景图片）这几层。

6.新增了分数系统，如果某种行为会给玩家加分，那么调用 ScoreBoard.AddScore(score)即可
击倒敌人和捡拾金币加分已完成。注意为每种敌人设置分数。

# 5.4 更新 -cys

- 修改 dash 参数，往上的力加大了

# 4.29 更新 -hyo

1.文件结构调整

Assets

- Animations 角色、UI 各种动画和动画控制器 Animator
- PhysicsMaterials 碰撞框的物理材质，比如摩擦力弹力这些
- Prefabs 预制模板，目前有敌人、玩家等，建议将所有需要复用的场景 GameObj 都做一个模板
- Scenes 关卡场景文件
- Scripts C#脚本文件
  - Camera 控制相机运动、动画的脚本
  - Enemy 控制敌人属性、行为的脚本，已经实现了基类 Enemy，其它类必须继承基类。
  - Player 玩家操作、属性、行为的脚本，基本上将其拖入 Player 这一 GameObj 下
  - RopeHook 实现钩锁功能的脚本，之后如果有其它工具，也可以将其改名为 Tool
  - UI 控制 UI 显示的脚本
- Sprites 所有 2D 素材，比如场景、玩家敌人动画、瓦片地图等。
  有些素材没有分入大类比较凌乱，其创作者可以自己将其归入以下大类中。
  - Enemy 敌人动画
  - Player 玩家动画
  - UI
  - Ground 场景
  - Materials 小材料，一些场景物件
  - Palettes 这是瓦片地图专用文件夹，熟悉瓦片地图的人操作。

有些文件我没有归类，请创作者自己将其归类

2.增加了相机跟随玩家移动的功能
将 CameraFollow 脚本附加在 MainCamera 下并将 target 绑定玩家
注意：对于每个关卡，都要预先设定相机的左下，右上边界以免越界
2D 画面的 Z 轴图层为-10，不能改变

3.解决了玩家会粘在墙上的问题

4.增加了 ReadMe 文档，之后更新可以汇总在这里

# 4.29 更新 -hyo

1.增加了玩家的生命-伤害系统和敌人的生命-伤害系统
（1）调用 PlayerHealth 的 DamagePlayer(damage)函数就可以伤害玩家，
在 Enemy 基类中已经写好了“敌人碰到玩家就会给予伤害的”功能，
如果后续还要增加“敌人攻击玩家给与伤害”，“陷阱伤害”部分，就需要调用函数
还可以调用 RecoverPlayer(recovery)来回复玩家
（2）调用 Enemy 基类的 TakeDamage(damageTaken)就可以伤害敌人，
这一点在 Player 类中已经写好
（3）增加了玩家受伤、死亡动画，注意死亡后的处理部分还没有完成

附调用方法：

（1）玩家 Player 受到伤害或回复

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

（2）敌人 Enemy 受到伤害

```
void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
```

2.增加了血条 UI

3.为了实验，完善了一个最简单的蝙蝠 UI，并且已经放入 prefab 作为模板，
需要时直接从 prefab 中拉进场景就行，拉进后注意设置以下两个属性
（1）设定 Bat 飞行范围（矩形）的左下角 LeftDownPos
（2）设定 Bat 飞行范围（矩形）的右上角 RightUpPos
