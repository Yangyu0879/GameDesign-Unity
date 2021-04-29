# 4.29 更新
1. 文件结构调整
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
这一点在Enemy基类中已经写好，负责写“陷阱伤害”部分的人注意就好
还可以调用RecoverPlayer(recovery)来回复玩家
（2）调用Enemy基类的TakeDamage(damageTaken)就可以伤害敌人，
这一点在Player类中已经写好
（3）增加了玩家受伤、死亡动画，注意死亡后的处理部分还没有完成
2.增加了血条UI
3.为了实验，完善了一个最简单的蝙蝠UI，并且已经放入prefab作为模板，
需要时直接从prefab中拉进场景就行，拉进后注意设置以下两个属性
（1）设定Bat飞行范围（矩形）的左下角LeftDownPos
（2）设定Bat飞行范围（矩形）的右上角RightUpPos
