using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityController))]
public class EntityIsSelected : MonoBehaviour
{
    [SerializeField] private Renderer _render;

    public bool _selected;
    private readonly List<Material> originalMaterials = new List<Material>();

    private EntityController _controller;

    // 초기화
    private void Awake()
    {
        _controller = GetComponent<EntityController>();
        _selected = false;

        originalMaterials.AddRange(_render.sharedMaterials);
    }

    // 외부에서 클릭시 이벤트
    public void OnClickObject(Material _outline)
    {
        if (_selected)
        {
            Unselected();
        }
        else
        {
            Selected(_outline);
        }
    }

    // 선택됨
    public void Selected(Material _outline)
    {
        if (_selected)
            return;

        _selected = true;

        // 외부 테두리 설정을 휘한 마테리얼 정렬
        var materials = new List<Material>();
        materials.AddRange(originalMaterials);
        materials.Add(_outline);
        
        // 마테리얼 설정
        _render.materials = materials.ToArray();

        // 매니저에 선택 결과 전송
        _controller.entityManager.SelectedEntity(this);
    }

    // 선택 해제
    public void Unselected(bool notifyManager = true)
    {
        if (!_selected)
            return;

        _selected = false;
        _render.materials = originalMaterials.ToArray();

        // 매니저에 선택 해제 (요청할때만)
        if(notifyManager)
            _controller.entityManager.UnselectedEntity(this);
    }
}
