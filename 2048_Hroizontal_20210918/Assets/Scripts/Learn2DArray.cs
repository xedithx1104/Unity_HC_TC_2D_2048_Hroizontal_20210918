using UnityEngine;
using System.Linq;  // LinQ Query �d�߻y�� - ��Ƭd��

/// <summary>
/// �{�ѤG���}�C
/// </summary>
public class Learn2DArray : MonoBehaviour
{
    // �@���}�C
    public int[] numbers = { 1, 7, 9, 10, 50 };
    // �G���}�C�G���|��� - �ˮM�� Odin �ۤv�g Editor
    public int[,] blocks = { { 2, 4 }, { 6, 8 } };

    public string[,] objects = new string[4, 6];

    public int[,] scores = { { 70, 90 }, { 77, 30 }, { 10, 20 } };

    private void Start()
    {
        #region �s��
        // �@���}�C�s��
        numbers[4] = 99;
        print("�@���}�C�Ĥ�����ơG" + numbers[4]);

        // �G���}�C�s��
        print("�G���}�C�ĤG�C�Ĥ@�� 1, 0�G" + blocks[1, 0]);
        blocks[1, 0] = 7;
        print("�G���}�C�ĤG�C�Ĥ@�� 1, 0�G" + blocks[1, 0]);

        print("�G���}�C�Ĥ@���ת��סG" + scores.GetLength(0));
        print("�G���}�C�ĤG���ת��סG" + scores.GetLength(1));

        string result = "";

        for (int i = 0; i < scores.GetLength(0); i++)
        {
            for (int j = 0; j < scores.GetLength(1); j++)
            {
                result += scores[i, j] + " | ";
            }

            result += "\n";
        }

        print(result);
        #endregion

        #region ��Ʒj�M
        // �j�M numbers �@���}�C���j�󵥩� 10 �����
        // var �L������ƫ��A
        // from ���A in �}�C    - �q�}�C�j�M��ƨëO�s�� ���A
        // where ���A ����      - �P�_ ���A �O�_�ŦX����
        // select ���A�F        - ��� �ŦX���� ���A
        var numberGratherTen =
            from int n in numbers
            where n >= 10
            select n;

        print("�ŦX >= 10 ��Ʀ��X���G" + numberGratherTen.Count());

        for (int i = 0; i < numberGratherTen.Count(); i++)
        {
            print(">= 10 ����Ƭ��G" + numberGratherTen.ToArray()[i]);
        }

        // scores ���ή檺���Ʀ�����
        var noPass =
            from int no in scores
            where no < 60
            select no;

        for (int i = 0; i < noPass.Count(); i++)
        {
            print("���ή檺���ơG" + noPass.ToArray()[i]);
        }
        #endregion
    }
}