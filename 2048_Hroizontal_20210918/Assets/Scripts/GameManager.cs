using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �C���޲z��
    /// </summary>
    [Header("�e������")]
    public GameObject goFinalCanvas;
    [Header("�������D")]
    public Text textFinalTitle;

    public void ShowFinalCanvas(bool win)
    {
        goFinalCanvas.SetActive(true);

        string title = win ? "Win" : "Lose";
        textFinalTitle.text = title;
    }
}
