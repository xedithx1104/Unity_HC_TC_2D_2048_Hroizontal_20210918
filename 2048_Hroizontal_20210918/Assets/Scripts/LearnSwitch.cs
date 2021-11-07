using UnityEngine;

/// <summary>
/// 判斷式 switch
/// </summary>
public class LearnSwitch : MonoBehaviour
{
    public string equipment;

    private void Start()
    {
        // Switch 語法：
        // switch (要判斷的資料)
        // {
        //     case 值 1:
        //         程式內容;
        //         break;
        //     case 值 2:
        //         程式內容;
        //         break;
        //     default:             // 當資料不符合上面 case 可省略
        //         程式內容;
        //         break;
        // }

        switch (equipment)
        {
            case "刀子":
                print("你現在拿著刀子");
                break;
            case "叉子":
                print("你現在拿著叉子");
                break;
            default:
                print("你拿的不是武器 =.=");
                break;
        }
    }
}
