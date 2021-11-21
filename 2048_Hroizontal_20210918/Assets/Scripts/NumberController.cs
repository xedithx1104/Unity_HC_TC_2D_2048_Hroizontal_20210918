using UnityEngine;

public class NumberController : MonoBehaviour
{
    public NumberManager numberManager;
    public Direction direction;

    private bool startClick;
    private Vector2 positionStart;
    private Vector2 positionEnd;
    private Vector2 v2Direction;

    private void Update()
    {
        ClickCheck();
    }

    private void ClickCheck()
    {
        if (!startClick && Input.GetKeyDown(KeyCode.Mouse0))
        {
            startClick = true;
            positionStart = Input.mousePosition;
        }
        else if (startClick && Input.GetKeyUp(KeyCode.Mouse0))
        {
            positionEnd = Input.mousePosition;
            v2Direction = positionEnd - positionStart;

            direction = DirectionCheck();

            numberManager.MoveBlock(direction);

            startClick = false;
        }
    }

    private Direction DirectionCheck()
    {
        Direction result = Direction.none;

        if (Mathf.Abs(v2Direction.x) > Mathf.Abs(v2Direction.y)) result = v2Direction.x > 0 ? Direction.right : Direction.left;
        else if (Mathf.Abs(v2Direction.x) < Mathf.Abs(v2Direction.y)) result = v2Direction.y > 0 ? Direction.up : Direction.down;

        return result;
    }
}

public enum Direction
{
    none, right, left, up, down
}