using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

/// <summary>
/// 2048 系統
/// 儲存每個區塊資料
/// 管理隨機生成
/// 滑動控制
/// 數字合併判定
/// 遊戲機制判定
/// </summary>
public class System2048 : MonoBehaviour
{
    [Header("空白區塊")]
    public Transform[] blocksEmpty;
    [Header("數字區塊")]
    public GameObject goNumberBlock;
    [Header("畫布 2048")]
    public Transform traCanvas2048;

    // 私人欄位顯示在屬性面板上
    [SerializeField]
    private Direction direction;
    private bool isClick;
    private Vector3 positionKeyDown;
    private Vector3 positionKeyUp;

    /// <summary>
    /// 所有區塊資料
    /// </summary>
    public BlockData[,] blocks = new BlockData[4, 4];

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CheckDirection();
    }

    /// <summary>
    /// 初始化資料
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
    /// 輸出區塊二維陣列資料
    /// </summary>
    private void PrintBlockData()
    {
        string result = "";

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                // 編號、數字與座標
                // result += "編號" + blocks[i, j].v2Index + " <color=red>數字：" + blocks[i, j].number + "</color> <color=yellow>" + blocks[i, j].v2Position + "</color> |";
                // 編號、數字與物件
                result += "編號" + blocks[i, j].v2Index + " <color=red>數字：" + blocks[i, j].number + "</color> <color=yellow>" + (blocks[i, j].goBlock ? "有" : "一") + "</color> |";
            }

            result += "\n";
        }

        print(result);
    }

    /// <summary>
    /// 建立隨機數字區塊
    /// 判斷所有區塊內沒有數字的區塊 - 數字為零
    /// 隨機挑選一個
    /// 生成數字放到該區塊內
    /// </summary>
    private void CreateRandomNumberBlock()
    {
        var equalZero =
            from BlockData data in blocks
            where data.number == 0
            select data;

        print("為零的資料有幾筆：" + equalZero.Count());

        int randomIndex = Random.Range(0, equalZero.Count());
        print("隨機編號：" + randomIndex);

        // 選出隨機區塊 編號
        BlockData select = equalZero.ToArray()[randomIndex];
        BlockData dataRandom = blocks[select.v2Index.x, select.v2Index.y];
        // 將數字 2 輸入到隨機區塊內
        dataRandom.number = 2;

        // 實例化-生成(物件，父物件)
        GameObject tempBlock = Instantiate(goNumberBlock, traCanvas2048);
        // 生成區塊 指定座標
        tempBlock.transform.position = dataRandom.v2Position;
        // 儲存 生成區塊 資料
        dataRandom.goBlock = tempBlock;

        PrintBlockData();
    }

    /// <summary>
    /// 檢查方向
    /// </summary>
    private void CheckDirection()
    {
        if (!isClick && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isClick = true;
            positionKeyDown = Input.mousePosition;
        }
        else if (isClick && Input.GetKeyUp(KeyCode.Mouse0))
        {
            isClick = false;
            positionKeyUp = Input.mousePosition;

            Vector3 directionValue = positionKeyUp - positionKeyDown;
            Vector2 normalized = directionValue.normalized;

            if (Mathf.Abs(normalized.x) > Mathf.Abs(normalized.y)) direction = normalized.x > 0 ? Direction.Right : Direction.Left;
            else if (Mathf.Abs(normalized.y) > Mathf.Abs(normalized.x)) direction = normalized.y > 0 ? Direction.Up : Direction.Down;
            else direction = Direction.None;

            if (direction != Direction.None) StartCoroutine(MoveBlock());
        }
    }

    /// <summary>
    /// 移動區塊
    /// </summary>
    private IEnumerator MoveBlock()
    {
        BlockData blockMove = new BlockData();
        BlockData blockCheck = new BlockData();
        bool canMove = false;

        switch (direction)
        {
            case Direction.Right:

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    for (int j = blocks.GetLength(1) - 2; j >= 0; j--)
                    {
                        if (blocks[i, j].number == 0) continue;

                        blockMove = blocks[i, j];

                        for (int k = j + 1; k < blocks.GetLength(1); k++)
                        {
                            if (blocks[i, k].number == 0 || blocks[i, k].number == blockMove.number)
                            {
                                canMove = true;
                                blockCheck = blocks[i, k];
                                continue;
                            }
                            else if (blocks[i, k].number != 0 && blocks[i, k].number != blockMove.number) break;
                        }

                        if (canMove)
                        {
                            canMove = false;
                            CanMoveAndMoveBlock(blockMove, blockCheck);
                        }

                        PrintBlockData();
                    }
                }

                break;
            case Direction.Left:

                for (int i = 0; i < blocks.GetLength(0); i++)
                {
                    for (int j = 1; j < blocks.GetLength(1); j++)
                    {
                        if (blocks[i, j].number == 0) continue;

                        blockMove = blocks[i, j];

                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (blocks[i, k].number == 0 || blocks[i, k].number == blockMove.number)
                            {
                                canMove = true;
                                blockCheck = blocks[i, k];
                                continue;
                            }
                            else if (blocks[i, k].number != 0 && blocks[i, k].number != blockMove.number) break;
                        }

                        if (canMove)
                        {
                            canMove = false;
                            CanMoveAndMoveBlock(blockMove, blockCheck);
                        }

                        PrintBlockData();
                    }
                }

                break;
            case Direction.Up:

                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    for (int j = 1; j < blocks.GetLength(0); j++)
                    {
                        if (blocks[j, i].number == 0) continue;

                        blockMove = blocks[j, i];

                        for (int k = j - 1; k >= 0; k--)
                        {

                            if (blocks[k, i].number == 0 || blocks[k, i].number == blockMove.number)
                            {
                                canMove = true;
                                blockCheck = blocks[k, i];
                            }
                            else if (blocks[k, i].number != 0 && blocks[k, i].number != blockMove.number) break;
                        }

                        if (canMove)
                        {
                            canMove = false;
                            CanMoveAndMoveBlock(blockMove, blockCheck);
                        }

                        PrintBlockData();
                    }
                }

                break;
            case Direction.Down:

                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    for (int j = blocks.GetLength(0) - 2; j >= 0; j--)
                    {
                        if (blocks[j, i].number == 0) continue;

                        blockMove = blocks[j, i];

                        for (int k = j + 1; k < blocks.GetLength(0); k++)
                        {

                            if (blocks[k, i].number == 0 || blocks[k, i].number == blockMove.number)
                            {
                                canMove = true;
                                blockCheck = blocks[k, i];
                                continue;
                            }
                            else if (blocks[k, i].number != 0 && blocks[k, i].number != blockMove.number) break;
                        }

                        if (canMove)
                        {
                            canMove = false;
                            CanMoveAndMoveBlock(blockMove, blockCheck);
                        }

                        PrintBlockData();
                    }
                }

                break;
        }



        yield return new WaitForSeconds(0.2f);

        CreateRandomNumberBlock();
    }

    /// <summary>
    /// 可以移動並移動區塊
    /// </summary>
    /// <param name="blockMove">要移動的區塊</param>
    /// <param name="blockCheck">檢查的區塊</param>
    private void CanMoveAndMoveBlock(BlockData blockMove, BlockData blockCheck)
    {
        blockMove.goBlock.transform.position = blockCheck.v2Position;

        if (blockCheck.number == blockMove.number)
        {
            int number = blockCheck.number + blockMove.number;
            blockCheck.number = number;
            blockCheck.goBlock.transform.Find("數字").GetComponent<Text>().text = number.ToString();
            blockMove.number = 0;
            Destroy(blockMove.goBlock);
            blockMove.goBlock = null;
        }
        else
        {
            blockCheck.number = blockMove.number;
            blockCheck.goBlock = blockMove.goBlock;
            blockMove.number = 0;
            blockMove.goBlock = null;
        }
    }
}

/// <summary>
/// 區塊資料
/// 每個區塊遊戲物件、座標、編號、數字
/// </summary>
public class BlockData
{
    /// <summary>
    /// 區塊內的數字物件
    /// </summary>
    public GameObject goBlock;
    /// <summary>
    /// 區塊座標
    /// </summary>
    public Vector2 v2Position;
    /// <summary>
    /// 區塊編號：二維陣列內的編號
    /// </summary>
    public Vector2Int v2Index;
    /// <summary>
    /// 區塊數字：預設為 0，或者 2、4、8....2048
    /// </summary>
    public int number;
}

/// <summary>
/// 方向列舉：無、右、左、上、下
/// </summary>
public enum Direction
{
    None, Right, Left, Up, Down
}