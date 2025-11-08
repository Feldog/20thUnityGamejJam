using UnityEngine;

public class PlayerCheckTrigger : MonoBehaviour
{
    [SerializeField] private EntityManager entityManager;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            entityManager.CheckNextFloor();
        }
    }
}
