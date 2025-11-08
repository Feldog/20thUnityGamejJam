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

    public void CheckNextFloor()
    {
        // 게임매니저에서 다음 플로어로 넘어갈수 있는지 판단
        var gameManager = GameManager.Instance;
        var result = gameManager.CheckNextFloor(CheckEntity());

        // 셀렉트 정리
        ClearEntity();
        if (result)
        {
            NextFloor();
            gameManager.NextFloor();
        }
    }

    public void NextFloor()
    {
        for (int i = 0; i < entitys.Count; i++)
        {
            entitys[i].RandomPose();
        }
    }

    public int CheckEntity()
    {
        if (selected != null && selected.Count >= 0)
        {
            int count = 0;
            foreach (var value in selected)
            {
                if(value.isEntity())
                {
                    value.gameObject.SetActive(false);
                    count++;
                }
            }
            return count;
        }
        else
        {
            return 0;
        }
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

    public void ClearEntity()
    {
        foreach(var entity in selected)
        {
            entity.Unselected(false);
        }
        selected.Clear();
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
