using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 遊戲管理器
    /// </summary>
    [Header("畫布結束")]
    public GameObject goFinalCanvas;
    [Header("結束標題")]
    public Text textFinalTitle;

    public void ShowFinalCanvas(bool win)
    {
        goFinalCanvas.SetActive(true);

        string title = win ? "Win" : "Lose";
        textFinalTitle.text = title;
    }
}
