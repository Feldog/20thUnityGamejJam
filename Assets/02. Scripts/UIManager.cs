using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject GameClearCanvas;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private FadeController fadeCanvas;

    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI entityText;


    public void SetPlayUI()
    {
        playCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void SetPauseUI()
    {
        pauseCanvas.SetActive(true);
    }

    public void SetGameClearUI()
    {
        GameClearCanvas.SetActive(true);
    }

    public void SetGameOverUI()
    {
        GameOverCanvas.SetActive(true);
    }

    public void FadeIn(float duration, Action action = null)
    {
        fadeCanvas.FadeIn(duration, action);
    }

    public void WaitFadeIn(float waitDuration, float fadeInDuration,Action action = null)
    {
        fadeCanvas.FadeOn(waitDuration, false, () => fadeCanvas.FadeIn(fadeInDuration, action));
    }

    public void SetFloorText(int floor)
    {
        floorText.text = $"Remain Floor : { floor }";
    }

    public void SetEntityText(int entity)
    {
        if (entity < 0)
            return;

        entityText.text = $"Remain Entity : {Mathf.Clamp(entity, 0, Define.entityMax)}";
    }
}
