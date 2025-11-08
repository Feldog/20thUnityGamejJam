using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

public class GameClearTrigger : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject endCamera;

    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInput>().enabled = false;
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        startCamera.SetActive(true);

        yield return new WaitForSeconds(2f);

        anim.SetTrigger(openAnim);

        yield return new WaitForSeconds(2f);

        endCamera.SetActive(true);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.GameClear();
    }
}
