using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

public class EntityManager : MonoBehaviour
{
    public List<EntityController> entitys;

    public List<EntityIsSelected> selected = new List<EntityIsSelected>();

    public void Start()
    {
        // 매니저에게 시작시 실행될 함수 전달
        GameManager.Instance.onStartGame += StartGame;
    }

    // 랜덤 Entity에게 숨겨진 캐릭으로 부여
    public void StartGame()
    {
        var randomEnemy = GetUniqueRandomsHashSet(0, entitys.Count, entityMax);

        for (int i = 0; i < entitys.Count; i++)
        {
            // 초기화
            entitys[i].Init();

            if (randomEnemy.Contains(i))
            {
                entitys[i].SetEntity(true);
            }
            else
            {
                entitys[i].SetEntity(false);
            }
        }
    }

    // 랜덤 Unique Number
    public List<int> GetUniqueRandomsHashSet(int min, int max, int count)
    {
        if (Mathf.Abs(max - min) < count)
        {
            return null;
        }

        HashSet<int> uniqueNum = new HashSet<int>();

        while(uniqueNum.Count < count)
        {
            uniqueNum.Add(Random.Range(min, max));
        }

        return uniqueNum.ToList();
    }

    // 플레이어가 새로 선택을 할때
    public void SelectedEntity(EntityIsSelected entity)
    {
        selected.Add(entity);

        while(selected.Count > maxSelected)
        {
            // 오래된 엔티티 삭제
            var oldEntity = selected[0];
            
            selected.RemoveAt(0);

            oldEntity.Unselected(false);
        }
    }
    
    // 플레이어가 직접 해제를 요청할때
    public void UnselectedEntity(EntityIsSelected entity)
    {
        if(selected.Contains(entity))
        {

            selected.Remove(entity);
        }
    }
}
