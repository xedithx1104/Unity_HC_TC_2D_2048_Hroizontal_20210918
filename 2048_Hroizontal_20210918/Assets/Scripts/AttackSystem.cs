using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 攻擊系統：近距離
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region 欄位：公開
    [Header("攻擊力基底")]
    public float attack = 10;
    [Header("攻擊目標")]
    public GameObject goTarget;
    [Header("攻擊力介面")]
    public Text textAttack;
    #endregion

    #region 事件
    private void Awake()
    {
        textAttack.text = "Atk " + attack;
    }
    #endregion

    #region 方法：公開
    // virtual 虛擬：允許子類別複寫
    /// <summary>
    /// 攻擊方法
    /// </summary>
    public virtual void Attack()
    {
        print("發動攻擊，攻擊力為：" + attack);
    }
    #endregion
}
