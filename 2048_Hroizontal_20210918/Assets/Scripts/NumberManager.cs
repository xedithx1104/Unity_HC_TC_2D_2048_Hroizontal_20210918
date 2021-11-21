using UnityEngine;
using System.Linq;

public class NumberManager : MonoBehaviour
{
    public BlockData[,] blocks = new BlockData[4, 4];
    public RectTransform[] emptyBlocks;
    public GameObject blockWithNuumber;
    public Transform canvasBlock;

    public int[] test = { 0, 1, 2, 3 };

    private void Start()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                blocks[i, j] = new BlockData();
                blocks[i, j].index = new Vector2Int(i, j);
                blocks[i, j].position = emptyBlocks[i * 4 + j].position;
            }
        }

        PrintAllBlock();

        CreateNumber();
        CreateNumber();
        PrintAllBlock();
    }

    private void PrintAllBlock()
    {
        string result = "";

        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = 0; j < blocks.GetLength(1); j++)
            {
                result += "( " + i + ", " + j + " ) - <color=red>" + blocks[i, j].number + "</color> | ";
            }

            result += "\n";
        }

        print(result);
    }

    private void CreateNumber()
    {
        var query = from BlockData i in blocks
                    where i.number == 0
                    select i;

        int r = Random.Range(0, query.Count());
        BlockData data = query.ToArray()[r];

        RectTransform tempBlock = Instantiate(blockWithNuumber, canvasBlock).GetComponent<RectTransform>();
        tempBlock.position = data.position;
        blocks[data.index.x, data.index.y].number = 2;
        blocks[data.index.x, data.index.y].targetBlock = tempBlock;
    }

    public void MoveBlock(Direction direction)
    {
        switch (direction)
        {
            case Direction.none:
                break;
            case Direction.right:
                MoveBlockAndCheck();
                break;
            case Direction.left:
                break;
            case Direction.up:
                break;
            case Direction.down:
                break;
            default:
                break;
        }
    }

    private void MoveBlockAndCheck()
    {
        for (int i = 0; i < blocks.GetLength(0); i++)
        {
            for (int j = blocks.GetLength(1) - 1; j >= 0; j--)
            {
                if (blocks[i, j].number == 0) continue;
                if (j == blocks.GetLength(1) - 1) continue;

                for (int k = blocks.GetLength(1) - 1; k != j; k--)
                {
                    if (blocks[i, k].number == 0)
                    {
                        blocks[i, k].targetBlock = blocks[i, j].targetBlock;
                        blocks[i, j].targetBlock = null;
                        blocks[i, k].number = blocks[i, j].number;
                        blocks[i, j].number = 0;
                        blocks[i, k].targetBlock.position = blocks[i, k].position;
                        break;
                    }
                }
            }
        }

        PrintAllBlock();
    }
}

public class BlockData
{
    public Vector2Int index;
    public int number;
    public Vector2 position;
    public RectTransform targetBlock;
}