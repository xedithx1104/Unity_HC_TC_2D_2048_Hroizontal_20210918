using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private AudioSource aud;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(AudioClip sound)
    {
        aud.PlayOneShot(sound);
    }

    /// <summary>
    /// 播放音效並隨機音量 0.8 ~ 1.2
    /// </summary>
    public void PlaySoundwithRandomVolume(AudioClip sound)
    {
        float r = Random.Range(0.8f, 1.2f);
        aud.PlayOneShot(sound, r);
    }
    
}
