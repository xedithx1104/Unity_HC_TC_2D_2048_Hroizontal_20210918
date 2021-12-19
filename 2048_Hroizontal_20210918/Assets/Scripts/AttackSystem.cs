using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;   // �ޥ� �t�� ���X API�G��P�{�� Coroutine

/// <summary>
/// �����t�ΡG��Z��
/// </summary>
public class AttackSystem : MonoBehaviour
{
    #region ���G���}
    [Header("�����O��")]
    public float attack = 10;
    [Header("�����ؼ�")]
    public GameObject goTarget;
    [Header("�����O����")]
    public Text textAttack;
    [Header("�������"), Range(0, 10)]
    public float delayAttack = 3.5f;
    [Header("����ǰe�ˮ`"), Range(0, 5)]
    public float delaySendDamage = 0.5f;
    [Header("�ʵe�Ѽ�")]
    public string parameterAttack = "����Ĳ�o";
    #endregion

    #region ���G�O�@ Protected
    // public ���\�������O�s��
    // private ���\�����O�s��
    // protected ���\�l���O�s��
    protected HealthSystem targetHealthSystem;
    protected Animator ani;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        textAttack.text = "Atk " + attack;
        ani = GetComponent<Animator>();
        targetHealthSystem = goTarget.GetComponent<HealthSystem>();
    }
    #endregion
    [Header("�����}�l�ƥ�")]
    public UnityEvent onAttackStart;
    [Header("���������ƥ�")]
    public UnityEvent onAttackFinish;

    #region ��k�G���}
    // virtual �����G���\�l���O�Ƽg
    /// <summary>
    /// ������k
    /// </summary>
    public virtual void Attack(float increase = 0)
    {
        // �Ұ� ��P�{��
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        // ���� 3.5 ��
        yield return new WaitForSeconds(delayAttack);
        // �����ʵe
        ani.SetTrigger(parameterAttack);
        // ���� 0.5 ��
        yield return new WaitForSeconds(delaySendDamage);
        onAttackStart.Invoke();
        // �ǰe�ˮ`
        targetHealthSystem.Hurt(attack);
        onAttackFinish.Invoke();
    }
    #endregion
}
