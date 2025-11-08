using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class IntroUI : MonoBehaviour
{
    public TextMeshProUGUI introText;

    public GameObject exitCamera;
    public float _duraiton;
    private Coroutine _cameraChanageRoutine;

    void Start()
    {
        introText.rectTransform.DOScale(1.3f, 1f).SetEase(Ease.InBounce).SetLoops(-1, LoopType.Yoyo);
        _cameraChanageRoutine = StartCoroutine(ChangeCamera());
    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            GameManager.Instance.Init();
            
            // 카메라 움직임을 멈춤
            StopCoroutine(_cameraChanageRoutine);
            _cameraChanageRoutine = null;

            // 카메라의 움직임 제어 해제
            if (exitCamera != null)
            {
                exitCamera.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }

    IEnumerator ChangeCamera()
    {
        while (true)
        {
            yield return new WaitForSeconds(_duraiton);

            if(exitCamera!=null)
            {
                exitCamera.SetActive(!exitCamera.activeSelf);
            }
        }
    }
}
