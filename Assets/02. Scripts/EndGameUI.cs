using DG.Tweening;
using TMPro;
using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    public float duration;
    private float _timer;

    public TextMeshProUGUI pressText;

    void Start()
    {
        pressText.rectTransform.DOScale(1.3f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo).SetUpdate(UpdateType.Normal, true);
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= duration)
        {
            _timer += Time.unscaledDeltaTime;
        }

        if (_timer > duration && Input.anyKeyDown)
        {
            GameManager.Instance.RestartCurrentScenes();
        }
    }
}
