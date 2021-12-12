using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 血量系統
/// 管理血量、受傷與死亡
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [Header("血量"), Range(0, 500)]
    public float hp = 100;
    [Header("要控制的血量與血條")]
    public Text textHp;
    public Image imgHp;
    [Header("造成傷害的物件標籤")]
    public string tagDamageObject;
    [Header("動畫參數")]
    public string parameterDamage = "受傷觸發";
    public string parameterDead = "死亡觸發";

    private float hpMax;
    private Animator ani;

    // 喚醒事件：在 Start 之前執行一次，處理初始值
    private void Awake()
    {
        hpMax = hp;
        ani = GetComponent<Animator>();
    }

    // 碰撞事件：兩個碰撞器其中一個有勾選 Is Trigger
    // Enter 碰撞開始時執行此事件一次
    // collision 碰到物件的碰撞資訊
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tagDamageObject) Hurt(10);
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收到的傷害</param>
    public void Hurt(float damage)
    {
        hp -= damage;
        textHp.text = "HP " + hp;
        imgHp.fillAmount = hp / hpMax;
        ani.SetTrigger(parameterDamage);
    }
}
