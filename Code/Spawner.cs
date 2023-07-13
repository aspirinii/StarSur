using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    public float levelTime; 
    float spawnTimer;
    int monterLevel;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }
    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        spawnTimer += Time.deltaTime;
        monterLevel = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTimer / levelTime), spawnData.Length - 1);

        if(spawnTimer > spawnData[monterLevel].spawnTime){
            Spawn();
            spawnTimer = 0;
        }

    }
    void Spawn()
    {
        int randomPointIndex = Random.Range(1, spawnPoints.Length); // 부모(Spawner)가 index가 0 이므로 빼줌 
        int randomMonsterIndex = Random.Range(0, 2); 
        // Max!Ex!clusive 확인 .. 0,1,2 중에 2는 제외됨 
        // 0,1,2 중에 0,1만 나옴

        Transform spawnPoint = spawnPoints[randomPointIndex];
        GameObject monster = GameManager.instance.pool.Get(0); // prefab 을 하나만 만들고 애니메이터만 바꿔줌
        monster.transform.position = spawnPoint.position;
        monster.GetComponent<Enemy>().Init(spawnData[monterLevel]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;

}
