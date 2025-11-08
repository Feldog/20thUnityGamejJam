using DG.Tweening;
using TMPro;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
    public TextMeshProUGUI introText;

    void Start()
    {
        introText.rectTransform.DOScale(1.3f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            GameManager.Instance.Init();
            gameObject.SetActive(false);
        }
    }
}
