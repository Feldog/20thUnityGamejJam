using UnityEngine;
using UnityEngine.UI;
using static Define;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var soundManager = SoundManager.Instance;
        bgmSlider.value = soundManager._volume[(int)EAudioType.BGM];
        sfxSlider.value = soundManager._volume[(int)EAudioType.SFX];

        bgmSlider.onValueChanged.AddListener((value) => soundManager.SetVolumeBGM(value));
        sfxSlider.onValueChanged.AddListener((value) => soundManager.SetVolumeSFX(value));
    }
}
