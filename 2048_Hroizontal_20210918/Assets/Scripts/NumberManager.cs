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
                blocks[i, j].index = new Vector2(i, j);
                blocks[i, j].position = emptyBlocks[i * 4 + j].position;

                //print("°}¦C®y¼Ð¡G" + i + ", " + j + " | " + "°}¦C®y¼Ð¡G" + blocks[i, j].position);
            }
        }

        CreateNumber();
    }

    private void CreateNumber()
    {
        var query = from BlockData i in blocks
                    where i.number == 0
                    select i;

        foreach (var item in query)
        {
            //print(item.position);
        }

        int r = Random.Range(0, query.Count());
        BlockData data = query.ToArray()[r];

        print("ÀH¾÷¡G" + data.position);
        RectTransform tempBlock = Instantiate(blockWithNuumber, canvasBlock).GetComponent<RectTransform>();
        tempBlock.position = data.position;
    }
}

public class BlockData
{
    public Vector2 index;
    public int number;
    public Vector2 position;
}