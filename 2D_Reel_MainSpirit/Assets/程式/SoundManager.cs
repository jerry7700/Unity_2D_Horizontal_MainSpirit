using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [Header("音樂管理")]
    public AudioMixer mixer;
    /// <summary>
    /// 背景音樂 音量
    /// </summary>
    /// <param name="v"></param>
    public void VolumeBGM(float v)
    {
        mixer.SetFloat("VolumeBGM", v);
    }
    /// <summary>
    /// 音效 音量 
    /// </summary>
    /// <param name="v"></param>
    public void VolumeSFX(float v)
    {
        mixer.SetFloat("VolumeSFX", v);
    }
}
