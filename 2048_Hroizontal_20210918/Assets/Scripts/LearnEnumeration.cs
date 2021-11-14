using UnityEngine;

/// <summary>
/// 列舉：下拉式選單
/// </summary>
public class LearnEnumeration : MonoBehaviour
{
    // 1. 定義列舉
    // 語法：修飾詞 列舉 列舉名稱 { 列舉值1，列舉值2,...列舉值n }
    public enum StateEnemy
    {
        Idle, Walk, Track, Attack, Dead
    }
    // 2. 使用列舉
    // 語法：修飾詞 列舉名稱 欄位名稱 指定 值；
    public StateEnemy state;

    private void Update()
    {
        switch (state)
        {
            case StateEnemy.Idle:
                print("等待中");
                break;
            case StateEnemy.Walk:
                print("走路中");
                break;
            case StateEnemy.Track:
                print("追蹤中");
                break;
            case StateEnemy.Attack:
                print("攻擊中");
                break;
            case StateEnemy.Dead:
                print("死亡中");
                break;
        }
    }
}
