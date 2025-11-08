using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class FadeController : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject fadeCanvas;

    private bool _isFade = false;

    // 화면이 암전되었다가 켜지는 효과
    public void FadeOutIn(float inDuration, float outDuration, float waitTime = 0f, Action action = null)
    {
        if (_isFade)
            return;

        _isFade = true;
        gameObject.SetActive(true);
        StartCoroutine(FadeOutInRoutine(inDuration, outDuration, waitTime, action));
    }

    private IEnumerator FadeOutInRoutine(float outDuration, float inDuration, float waitTime, Action action = null)
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1, outDuration);

        yield return new WaitForSeconds(waitTime + outDuration);

        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.DOFade(0, inDuration).OnComplete(() =>
        {
            action?.Invoke();
            _isFade = false;
        });
    }

    // 화면이 켜지는 효과
    public void FadeIn(float duration, Action action = null)
    {
        if (_isFade)
            return;

        _isFade = true;
        gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 1);
        fadeImage.DOFade(0, duration).OnComplete(() =>
        {
            action?.Invoke();
            _isFade = false;
        });
    }

    // 화면이 꺼지는 효과
    public void FadeOut(float duration, Action action = null)
    {
        if (_isFade)
            return;

        gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1, duration).OnComplete(() =>
        {
            action?.Invoke();
            _isFade = false;
        });
    }

    public void FadeOn(float duration, bool endOn = true, Action action = null)
    {
        if(_isFade)
            return;

        fadeImage.DOKill();
        gameObject.SetActive(true);

        StartCoroutine(SetFade(duration, 1f, endOn, action));
    }

    // 페이드 강제 종료
    public void FadeOff()
    {
        _isFade = false;

        fadeImage.DOKill();
        gameObject.SetActive(false);
    }

    IEnumerator SetFade(float duration, float alpha, bool endOn, Action action)
    {
        _isFade = true;
        fadeImage.color = new Color(0, 0, 0, alpha);

        yield return new WaitForSeconds(duration);
        
        // 페이드 재사용 가능
        _isFade = false;
        
        // 사용이 끝나고 종료 (선택사항)
        if(endOn)
            gameObject.SetActive(false);

        // 액션이 있으면 실행
        action?.Invoke();
    }
}