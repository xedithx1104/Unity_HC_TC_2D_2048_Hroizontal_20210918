using UnityEngine;

public class LearnPropertyStatic : MonoBehaviour
{
    private void Start()
    {
        // 靜態屬性 Static Property
        // 取得靜態屬性
        // 語法：類別名稱.靜態屬性名稱
        print("隨機值：" + Random.value);
        print("攝影機總數：" + Camera.allCamerasCount);

        // 設定靜態屬性
        // 語法：類別名稱.靜態屬性名稱 指定 值；
        Cursor.visible = false;
        // Mathf.PI = 9.99999f;        // (Read Only) 不能設定 唯讀屬性
    }
}
