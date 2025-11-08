using UnityEngine;
using UnityEngine.Audio;
using static Define;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioMixer audioMixer;

    // 매니저에서 관리할 Mute, Volume 데이터
    public bool[] _mute = new bool[3] { false, false, false };
    public float[] _volume = new float[3] { 70f, 70f, 70f };


    protected override void Awake()
    {
        base.Awake();

        if (audioMixer == null)
        {
            audioMixer = Resources.Load<AudioMixer>("MainMixer");
            if (audioMixer == null)
            {
                Debug.Log("audioMixer를 찾을수 없습니다.");
            }
        }
    }

    public void Start()
    {
        // 여기에 데이터 로드


        // 데이터 적용
    }

    // 오디오 믹서의 그룹을 찾는 함수
    public AudioMixerGroup FindAudioMixerOutput(EAudioType type)
    {
        AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(type.ToString());
        if (groups.Length > 0)
        {
            return groups[0];
        }
        return null;
    }

    private void SetAudioVolume(EAudioType audioType, float value)
    {
        _volume[(int)audioType] = value;

        if (_mute[(int)audioType])
            return;

        float volume = -80f;
        if (value > 0.01f)
        {
            volume = Mathf.Log10(value / 100f) * 20f;
        }
        if (!_mute[(int)audioType])
        {
            audioMixer.SetFloat(audioType.ToString(), volume);
        }
    }

    private void SetAudioMute(EAudioType audioType)
    {
        int type = (int)audioType;

        if (_mute[type])
        {
            // 기존 뮤트 상태였을시 뮤트를 해제하고 Volume을 적용
            _mute[type] = false;
            SetAudioVolume(audioType, _volume[type]);
        }
        else
        {
            // 뮤트 상태를 적용
            _mute[type] = true;
            audioMixer.SetFloat(audioType.ToString(), -80f);
        }
    }

    public float GetVolume(EAudioType type)
    {
        int typeIndex = (int)type;
        if (typeIndex < 0 || typeIndex >= _volume.Length) return 0f;

        float logVolume = _volume[typeIndex];

        if (logVolume <= -80f)
        {
            return 0f;
        }

        float sliderValue = Mathf.Pow(10f, logVolume / 20f) * 100f;

        return sliderValue;
    }

    public bool IsMuted(EAudioType type)
    {
        int typeIndex = (int)type;
        if (typeIndex < 0 || typeIndex >= _mute.Length) return false;

        return _mute[typeIndex];
    }

    // 외부에서 참조해서 사용될 볼륨
    public void SetVolumeBGM(float value)
    {
        SetAudioVolume(EAudioType.BGM, value);
    }
    public void SetMuteBGM()
    {
        SetAudioMute(EAudioType.BGM);
    }
    public void SetVolumeSFX(float value)
    {
        SetAudioVolume(EAudioType.SFX, value);
    }
    public void SetMuteSFX()
    {
        SetAudioMute(EAudioType.SFX);
    }
    public void SetVolumeMaster(float value)
    {
        SetAudioVolume(EAudioType.MASTER, value);
    }
    public void SetMuteMaster()
    {
        SetAudioMute(EAudioType.MASTER);
    }
}
