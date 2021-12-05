using UnityEngine;

/// <summary>
/// 攻擊系統：遠距離
/// 繼承語法：子類別 : 要繼承的類別(父類別)
/// 擁有父類別的成員：欄位、屬性、方法、事件
/// </summary>
public class AttackSystemFar : AttackSystem
{
    [Header("生成粒子位置")]
    public Transform positionSpawn;
    [Header("攻擊粒子")]
    public GameObject goAttackParticle;
}
