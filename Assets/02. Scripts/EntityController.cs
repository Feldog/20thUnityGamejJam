
using UnityEngine;
using static Define;

[RequireComponent(typeof(Animator))]
public class EntityController : MonoBehaviour
{
    public EntityManager entityManager;

    private Animator _anim;

    private int _poseIndex = 0;
    public bool _isEntity;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Init()
    {
        gameObject.SetActive(true);

        _isEntity = false;

        // 랜덤 값
        _poseIndex = Random.Range(0, PoseCount);
        _poseIndex %= PoseCount;

        // 포즈의 인덱스를 변경후 실행
        _anim.SetInteger(entityPoseIndexAnim, _poseIndex);
        _anim.SetTrigger(entitySetPoseAnim);
    }

    public void RandomPose()
    {
        // 찾아야하는 대상은 애니메이션이 변하지 않음
        if (_isEntity)
            return;

        // 이전 포즈랑 겹치지 않게 랜덤한 포징 실행
        _poseIndex += Random.Range(1, PoseCount - 1);
        _poseIndex %= PoseCount;

        // 포즈의 인덱스를 변경후 실행
        _anim.SetInteger(entityPoseIndexAnim, _poseIndex);
        _anim.SetTrigger(entitySetPoseAnim);
    }

    // 엔티티 설정
    public void SetEntity(bool isEntity)
    {
        _isEntity = isEntity;
    }
}
