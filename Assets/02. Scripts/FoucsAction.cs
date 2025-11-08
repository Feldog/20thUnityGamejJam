using UnityEngine;

public class FoucsAction : MonoBehaviour
{
    public Material selectedMaterial;
    public LayerMask entityLayer;

    public Transform playerCamera;

    public float interactiveDistance = 3f;

    private RaycastHit[] _hits = new RaycastHit[1];
    private int _hitCount;

    private AudioSource _audioSource;
    public AudioClip[] selectedSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // 화면 정중앙의 레이
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        _hitCount = Physics.RaycastNonAlloc(ray, _hits, interactiveDistance, entityLayer);
        if( _hitCount > 0 && Input.GetMouseButtonDown(0))
        {
            PlaySelectedSound();
            _hits[0].collider.GetComponent<EntityIsSelected>().OnClickObject(selectedMaterial);
        }
    }

    private void PlaySelectedSound()
    {
        if(_audioSource != null && selectedSound != null && selectedSound.Length > 0)
        {
            _audioSource.PlayOneShot(selectedSound[Random.Range(0, selectedSound.Length)]);
        }
    }
}
