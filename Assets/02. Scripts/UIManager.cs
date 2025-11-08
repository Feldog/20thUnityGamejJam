using System;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private FadeController fadeCanvas;

    [SerializeField] private TextMeshProUGUI floorText;
    [SerializeField] private TextMeshProUGUI entityText;

    public void Update()
    {
        
    }
    public void SetPlayUI()
    {
        playCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void SetPauseUI()
    {
        pauseCanvas.SetActive(true);
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
        entityText.text = $"Remain Entity : {entity}";
    }
}
