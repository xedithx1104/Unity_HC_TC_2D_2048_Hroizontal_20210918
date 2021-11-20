using UnityEngine;

public class NumberController : MonoBehaviour
{
    public NumberManager numberManager;

    private bool startClick;
    private Vector2 positionStart;
    private Vector2 positionEnd;

    private void Update()
    {
        Direction();
    }

    private void Direction()
    {
        if (!startClick && Input.GetKeyDown(KeyCode.Mouse0))
        {
            startClick = true;
            positionStart = Input.mousePosition;
        }
        else if (startClick && Input.GetKeyUp(KeyCode.Mouse0))
        {
            positionEnd = Input.mousePosition;
            print(positionEnd - positionStart);

            Vector2 direction = positionEnd - positionStart;
            print(direction.normalized.x);
            print(direction.normalized.y);

            startClick = false;
        }
    }
}
