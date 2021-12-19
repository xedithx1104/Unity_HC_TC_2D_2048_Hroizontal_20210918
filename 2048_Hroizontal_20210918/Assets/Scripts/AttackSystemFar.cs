using UnityEngine;

/// <summary>
/// �����t�ΡG���Z��
/// �~�ӻy�k�G�l���O : �n�~�Ӫ����O(�����O)
/// �֦������O�������G���B�ݩʡB��k�B�ƥ�
/// </summary>
public class AttackSystemFar : AttackSystem
{
    [Header("�ͦ��ɤl��m")]
    public Transform positionSpawn;
    [Header("�����ɤl")]
    public GameObject goAttackParticle;
    [Header("�ɤl�o�g�t��"), Range(0, 1500)]
    public float speed = 500;

    // override �Ƽg�G�Ƽg�����O virtual ����
    public override void Attack(float increase = 0)
    {
        // base.Attack();      // base �򩳡G�����O�����e

        // �ͦ�(����A�y�СA����)
        // �ͦ�������W�٫��|�� (Clone)
        // Quaternion �|����
        // identity �s����
        GameObject tempAttack = Instantiate(goAttackParticle, positionSpawn.position, Quaternion.identity);
        tempAttack.GetComponent<Rigidbody2D>().AddForce(new Vector2(speed, 0));
        // �K�[����<�l�u�t��>().�����O = �������t�Χ����O
        tempAttack.AddComponent<Bullet>().attack = attack + increase;

        print("���������O�G" + (attack + increase));
    }
}
