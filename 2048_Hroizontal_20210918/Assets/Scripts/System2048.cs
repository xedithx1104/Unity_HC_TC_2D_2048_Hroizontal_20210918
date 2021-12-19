using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;       // �ޥ� Unity �ƥ� �R�W�Ŷ�
using System.Linq;

/// <summary>
/// 2048 �t��
/// �x�s�C�Ӱ϶����
/// �޲z�H���ͦ�
/// �ưʱ���
/// �Ʀr�X�֧P�w
/// �C������P�w
/// </summary>
public class System2048 : MonoBehaviour
{
    #region ���G���}
    [Header("�ťհ϶�")]
    public Transform[] blocksEmpty;
    [Header("�Ʀr�϶�")]
    public GameObject goNumberBlock;
    [Header("�e�� 2048")]
    public Transform traCanvas2048;
    [Header("�Ʀr�ۦP�X�֨ƥ�")]
    public OnSameNumberCombine onSameNumberCombine;
    [Header("�ĤH�^�X�ƥ�")]
    public UnityEvent onEnemyTurn;
    #endregion

    [System.Serializable]
    public class OnSameNumberCombine : UnityEvent<float> { }
    #region ���G�p�H
    // �p�H�����ܦb�ݩʭ��O�W
    [SerializeField]
    private Direction direction;
    [SerializeField]
    private StateTurn stateTurm;
    /// <summary>
    /// �Ҧ��϶����
    /// </summary>
    private BlockData[,] blocks = new BlockData[4, 4];

    /// <summary>
    /// ���U�y��
    /// </summary>
    private Vector3 posDown;
    /// <summary>
    /// ��}�y��
    /// </summary>
    private Vector3 posUp;
    /// <summary>
    /// �O�_���U����
    /// </summary>
    private bool isClickMouse;
    #endregion

    #region �ƥ�
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (stateTurm == StateTurn.My) CheckDirection();
    }
    #endregion

    #region ��k�G�p�H
    /// <summary>
    /// ��l�Ƹ��
    /// </summary>
    private void Initialize()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                blocks[i, j] = new BlockData();
                blocks[i, j].v2Index = new Vector2Int(i, j);
                blocks[i, j].v2Position = blocksEmpty[i * blocks.GetLength(1) + j].position;
            }
        }

        PrintBlockData();
        CreateRandomNumberBlock();
        CreateRandomNumberBlock();
    }

    /// <summary>
    /// ��X�϶��G���}�C���
    /// </summary>
    private void PrintBlockData()
    {
        string result = "";

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                // �s���B�Ʀr�P�y��
                // result += "�s��" + blocks[i, j].v2Index + " <color=red>�Ʀr�G" + blocks[i, j].number + "</color> <color=yellow>" + blocks[i, j].v2Position + "</color> |";
                // �s���B�Ʀr�P����
                // �T���B��l
                // �y�k�G���L�� ? �� A �G �� B�F
                // ���L�Ȭ� true ���G�� �� A
                // ���L�Ȭ� false ���G�� �� B
                result += "�s��" + blocks[i, j].v2Index + " <color=red>�Ʀr�G" + blocks[i, j].number + "</color> <color=yellow>" + (blocks[i, j].goBlock ? "��" : "�@") + "</color> |";
            }

            result += "\n";
        }

        print(result);
    }

    /// <summary>
    /// �إ��H���Ʀr�϶�
    /// �P�_�Ҧ��϶����S���Ʀr���϶� - �Ʀr���s
    /// �H���D��@��
    /// �ͦ��Ʀr���Ӱ϶���
    /// </summary>
    private void CreateRandomNumberBlock()
    {
        var equalZero =
            from BlockData data in blocks
            where data.number == 0
            select data;

        print("���s����Ʀ��X���G" + equalZero.Count());

        int randomIndex = Random.Range(0, equalZero.Count());
        print("�H���s���G" + randomIndex);

        // ��X�H���϶� �s��
        BlockData select = equalZero.ToArray()[randomIndex];
        BlockData dataRandom = blocks[select.v2Index.x, select.v2Index.y];
        // �N�Ʀr 2 ��J���H���϶���
        dataRandom.number = 2;

        // ��Ҥ�-�ͦ�(����A������)
        GameObject tempBlock = Instantiate(goNumberBlock, traCanvas2048);
        // �ͦ��϶� ���w�y��
        tempBlock.transform.position = dataRandom.v2Position;
        // �x�s �ͦ��϶� ���
        dataRandom.goBlock = tempBlock;

        PrintBlockData();
    }

    /// <summary>
    /// �ˬd��V
    /// </summary>
    private void CheckDirection()
    {
        #region ��L
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Direction.Up;
            CheckAndMoveBlock();
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Direction.Down;
            CheckAndMoveBlock();
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Direction.Left;
            CheckAndMoveBlock();
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Direction.Right;
            CheckAndMoveBlock();
        }
        #endregion

        #region �ƹ��PĲ��
        if (!isClickMouse && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isClickMouse = true;
            posDown = Input.mousePosition;
            //print("���U�y�СG" + posDown);
        }
        else if (isClickMouse && Input.GetKeyUp(KeyCode.Mouse0))
        {
            isClickMouse = false;
            posUp = Input.mousePosition;
            //print("��}�y�СG" + posUp);

            // 1. �p��V�q�� ��}�y�� - ���U�y��
            Vector3 directionValue = posUp - posDown;
            //print("�V�q�ȡG" + directionValue);
            // 2. �ഫ�� 0 ~ 1 ��
            //print("�ഫ��ȡG" + directionValue.normalized);

            // ��V X �P Y �������
            float xAbs = Mathf.Abs(directionValue.normalized.x);
            float yAbs = Mathf.Abs(directionValue.normalized.y);
            // X > Y ������V
            if (xAbs > yAbs)
            {
                if (directionValue.normalized.x > 0) direction = Direction.Right;
                else direction = Direction.Left;
                CheckAndMoveBlock();
            }
            // Y > X ������V
            else if (yAbs > xAbs)
            {
                if (directionValue.normalized.y > 0) direction = Direction.Up;
                else direction = Direction.Down;
                CheckAndMoveBlock();
            }
        }
        #endregion
    }

    

    /// <summary>
    /// �ˬd�ò��ʰ϶�
    /// </summary>
    private void CheckAndMoveBlock()
    {
        print("�ثe����V�G" + direction);
        BlockData blockOriginal = new BlockData();      // ��l���Ʀr���϶�
        BlockData blockCheck = new BlockData();         // �ˬd���䪺�϶�
        bool canMove = false;                           // �O�_�i�H���ʰ϶�
        bool sameNumber = false;                        // �O�_�ۦP�Ʀr
        int sameNumberCount = 0;                        // �ۦP�Ʀr�X�֦���
        bool canMoveBlockAll = false;                   // �O�_�����i�H���ʰ϶�

        switch (direction)
        {
            case Direction.Right:

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    sameNumberCount = 0;                                    // �ۦP�Ʀr�X�֦����k�s

                    for (int j = blocks.GetLength(1) - 2; j >= 0; j--)
                    {
                        blockOriginal = blocks[i, j];

                        // �p�G �Ӱ϶����Ʀr ���s �N �~�� (���L���j�����U�Ӱj��)
                        if (blockOriginal.number == 0) continue;

                        for (int k = j + 1; k < blocks.GetLength(1) - sameNumberCount; k++)
                        {
                            if (blocks[i, k].number == 0)
                            {
                                blockCheck = blocks[i, k];
                                canMove = true;
                            }
                            else if (blocks[i, k].number == blockOriginal.number)
                            {
                                blockCheck = blocks[i, k];
                                canMove = true;
                                sameNumber = true;
                                sameNumberCount++;
                            }
                            // �_�h �p�G �ˬd�϶� ���Ʀr �P �쥻�϶� ���Ʀr ���ۦP �N�����ʡB�Ʀr���ۦP�ä��_
                            else if (blocks[i, k].number != blockOriginal.number)
                            {
                                break;
                            }
                        }

                        // �p�G �i�H���� �b���� ���ʰ϶�(��l�A�ˬd�A�O�_�ۦP�Ʀr)
                        if (canMove)
                        {
                            canMoveBlockAll = true;                                 // �����϶����i�H����
                            canMove = false;
                            MoveBlock(blockOriginal, blockCheck, sameNumber);
                            sameNumber = false;
                        }
                    }
                }

                break;
            case Direction.Left:

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    sameNumberCount = 0;

                    for (int j = 1; j < blocks.GetLength(1); j++)
                    {
                        blockOriginal = blocks[i, j];

                        // �p�G �Ӱ϶����Ʀr ���s �N �~�� (���L���j�����U�Ӱj��)
                        if (blockOriginal.number == 0) continue;

                        for (int k = j - 1; k >= 0 + sameNumberCount; k--)
                        {
                            if (blocks[i, k].number == 0)
                            {
                                blockCheck = blocks[i, k];
                                canMove = true;
                            }
                            else if (blocks[i, k].number == blockOriginal.number)
                            {
                                blockCheck = blocks[i, k];
                                canMove = true;
                                sameNumber = true;
                                sameNumberCount++;
                            }
                            else if (blocks[i, k].number != blockOriginal.number)
                            {
                                break;
                            }
                        }

                        // �p�G �i�H���� �b���� ���ʰ϶�(��l�A�ˬd�A�O�_�ۦP�Ʀr)
                        if (canMove)
                        {
                            canMoveBlockAll = true;
                            canMove = false;
                            MoveBlock(blockOriginal, blockCheck, sameNumber);
                            sameNumber = false;
                        }
                    }
                }

                break;
            case Direction.Up:

                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    sameNumberCount = 0;

                    for (int j = 1; j < blocks.GetLength(0); j++)
                    {
                        blockOriginal = blocks[j, i];

                        // �p�G �Ӱ϶����Ʀr ���s �N �~�� (���L���j�����U�Ӱj��)
                        if (blockOriginal.number == 0) continue;

                        for (int k = j - 1; k >= 0 + sameNumberCount; k--)
                        {
                            if (blocks[k, i].number == 0)
                            {
                                blockCheck = blocks[k, i];
                                canMove = true;
                            }
                            else if (blocks[k, i].number == blockOriginal.number)
                            {
                                blockCheck = blocks[k, i];
                                canMove = true;
                                sameNumber = true;
                                sameNumberCount++;
                            }
                            else if (blocks[k, i].number != blockOriginal.number)
                            {
                                break;
                            }
                        }

                        // �p�G �i�H���� �b���� ���ʰ϶�(��l�A�ˬd�A�O�_�ۦP�Ʀr)
                        if (canMove)
                        {
                            canMoveBlockAll = true;
                            canMove = false;
                            MoveBlock(blockOriginal, blockCheck, sameNumber);
                            sameNumber = false;
                        }
                    }
                }

                break;
            case Direction.Down:

                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    sameNumberCount = 0;

                    for (int j = blocks.GetLength(0) - 2; j >= 0; j--)
                    {
                        blockOriginal = blocks[j, i];

                        // �p�G �Ӱ϶����Ʀr ���s �N �~�� (���L���j�����U�Ӱj��)
                        if (blockOriginal.number == 0) continue;

                        for (int k = j + 1; k < blocks.GetLength(0) - sameNumberCount; k++)
                        {
                            if (blocks[k, i].number == 0)
                            {
                                blockCheck = blocks[k, i];
                                canMove = true;
                            }
                            else if (blocks[k, i].number == blockOriginal.number)
                            {
                                blockCheck = blocks[k, i];
                                canMove = true;
                                sameNumber = true;
                                sameNumberCount++;
                            }
                            else if (blocks[k, i].number != blockOriginal.number)
                            {
                                break;
                            }
                        }

                        // �p�G �i�H���� �b���� ���ʰ϶�(��l�A�ˬd�A�O�_�ۦP�Ʀr)
                        if (canMove)
                        {
                            canMoveBlockAll = true;
                            canMove = false;
                            MoveBlock(blockOriginal, blockCheck, sameNumber);
                            sameNumber = false;
                        }
                    }
                }

                break;
        }

        if (canMoveBlockAll)
        {
            onEnemyTurn.Invoke();
            stateTurm = StateTurn.Enemy;
            CreateRandomNumberBlock();                      // ���ʫ� �ͦ��U�@���϶�
        }
        else
        {
            print("���ಾ��");
        }
    }

    /// <summary>
    /// ���ʰ϶�
    /// </summary>
    /// <param name="blockOriginal">��l���϶��A�n�Q���ʪ�</param>
    /// <param name="blockCheck">�ˬd���϶��A��l�϶����䪺�϶�</param>
    /// <param name="sameNumber">�O�_�ۦP�Ʀr</param>
    private void MoveBlock(BlockData blockOriginal, BlockData blockCheck, bool sameNumber)
    {
        #region ���ʰ϶�
        // �N�쥻�����󲾰ʨ��ˬd�Ʀr���s���϶���m
        // �N�쥻�Ʀr�k�s�A�ˬd�Ʀr�ɤW
        // �N�쥻������M�šA�ˬd����ɤW
        blockOriginal.goBlock.transform.position = blockCheck.v2Position;

        if (sameNumber)
        {
            int number = blockCheck.number * 2;
            blockCheck.number = number;

            Destroy(blockOriginal.goBlock);
            blockCheck.goBlock.transform.Find("�Ʀr").GetComponent<Text>().text = number.ToString();

            // �ۦP�Ʀr�X�֨ƥ� Ĳ�o
            onSameNumberCombine.Invoke(number);
        }
        else
        {
            blockCheck.number = blockOriginal.number;
            blockCheck.goBlock = blockOriginal.goBlock;
        }

        blockOriginal.number = 0;
        blockOriginal.goBlock = null;
        #endregion

        PrintBlockData();
    }
    #endregion

    #region ��k�G���}
    /// <summary>
    /// ������ڤ�^�X
    /// </summary>
    public void ChangeToMyTurn()
    {
        stateTurm = StateTurn.My;
    }
    #endregion
}

/// <summary>
/// �϶����
/// �C�Ӱ϶��C������B�y�СB�s���B�Ʀr
/// </summary>
public class BlockData
{
    /// <summary>
    /// �϶������Ʀr����
    /// </summary>
    public GameObject goBlock;
    /// <summary>
    /// �϶��y��
    /// </summary>
    public Vector2 v2Position;
    /// <summary>
    /// �϶��s���G�G���}�C�����s��
    /// </summary>
    public Vector2Int v2Index;
    /// <summary>
    /// �϶��Ʀr�G�w�]�� 0�A�Ϊ� 2�B4�B8....2048
    /// </summary>
    public int number;
}

/// <summary>
/// ��V�C�|�G�L�B�k�B���B�W�B�U
/// </summary>
public enum Direction
{
    None, Right, Left, Up, Down
}

/// <summary>
/// �^�X���A�G�ڤ�B�Ĥ�
/// </summary>
public enum StateTurn
{
    My, Enemy
}