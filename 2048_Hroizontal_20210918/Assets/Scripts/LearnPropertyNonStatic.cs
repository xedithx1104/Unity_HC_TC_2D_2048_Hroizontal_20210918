using UnityEngine;

/// <summary>
/// 學習非靜態屬性、方法
/// </summary>
public class LearnPropertyNonStatic : MonoBehaviour
{
    // 靜態與非靜態差異：
    // 1. 定義欄位
    // 2. 欄位資料類型與類別相同
    // 3. 欄位要儲存物件
    public Camera cam;
    public Transform tra;

    private void Start()
    {
        // 靜態對照組
        print("攝影機數量：" + Camera.allCamerasCount);

        // 非靜態屬性
        // 取得非靜態屬性語法：
        // 欄位名稱.非靜態屬性名稱
        print("攝影機深度：" + cam.depth);

        // 設定非靜態屬性語法：
        // 欄位名稱.非靜態屬性名稱 指定 值；
        tra.position = new Vector3(5, 0, 0);
    }

    private void Update()
    {
        // 呼叫非靜態方法語法：
        // 欄位名稱.非靜態方法名稱(對應的引數)
        tra.Translate(1, 0, 0);
    }
}
