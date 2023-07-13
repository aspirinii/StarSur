using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 프리팹 보관할 변수
    public GameObject[] prefabs; // 배열
    
    // 풀 담당 하는 리스트들
    List<GameObject>[] pools; //리스트 배열 

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for(int index = 0; index < prefabs.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }

    }

    public GameObject Get(int index){
        GameObject select = null;
        // 선택한 풀의 놀고있는 게임오브젝트 접근
        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // 발견하면 select 변수에 할당

        // 못찾았으면 새로 생성해서 할당
        if(!select){
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
            //pool[index] 하나의 Prefabs(Enemy)
        } 
        // select 변수에 할당된 게임오브젝트를 활성화
        return select;
    }

}
