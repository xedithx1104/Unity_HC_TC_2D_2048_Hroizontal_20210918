using UnityEngine;
using UnityEngine.Audio;            // 引用 音效 命名空間
using UnityEngine.SceneManagement;  // 引用 場景管理 命名空間

/// <summary>
/// 開始畫面選單管理器
/// 開始遊戲、設定、離開遊戲
/// </summary>
public class MenuManager : MonoBehaviour
{
    // Unity 按鈕與程式溝通
    // 1. 公開的方法
    // 2. 按鈕設定點擊事件 On Click

    public AudioMixer mixer;

    /// <summary>
    /// 開始遊戲
    /// </summary>
    public void StartGame()
    {
        // 場景管理.載入場景(場景名稱)
        SceneManager.LoadScene("遊戲場景");
        // SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 設定遊戲
    /// </summary>
    public void SettingGameBGM(float volume)
    {
        mixer.SetFloat("音量BGM", volume);
    }

    /// <summary>
    /// 設定遊戲
    /// </summary>
    public void SettingGameSFX(float volume)
    {
        mixer.SetFloat("音量SFX", volume);
    }

    /// <summary>
    /// 離開遊戲
    /// </summary>
    public void QuitGame()
    {
        // 應用程式.離開()；
        // Quit 不會再 Editor 執行，發布執行檔 手機、PC
        Application.Quit();

        print("離開遊戲~");
    }
}
