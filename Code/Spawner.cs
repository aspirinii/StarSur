using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public MonsterData[] monsterData;
    public float levelTime; 
    float spawnTimer;
    float frequencyOfSpawn = 0.3f;


    private SpawnerMonsterData[] spawnerMonsterArray;

    struct SpawnerMonsterData
    {
        public int wavePhase;
        public int monsterLevel;
        public float startingTime;
        public bool spawnerStarted;

        public SpawnerMonsterData(int wavePhase, int monsterLevel, float startingTime)
        {
            this.wavePhase = wavePhase;
            this.monsterLevel = monsterLevel;
            this.startingTime = startingTime;
            this.spawnerStarted = false;
        }
    }
    
    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        spawnerMonsterArray = new SpawnerMonsterData[]
        {
            new SpawnerMonsterData(1, 0, 0.0f),
            new SpawnerMonsterData(2, 1, 10.0f),
            new SpawnerMonsterData(3, 2, 20.0f),
            new SpawnerMonsterData(4, 3, 30.0f),
            new SpawnerMonsterData(5, 4, 40.0f)
        };

        // Use the array as needed
        foreach (var monsterData in spawnerMonsterArray)
        {
            Debug.Log($" ### Check Wave ### Wave: {monsterData.wavePhase}, Level: {monsterData.monsterLevel}, Time: {monsterData.startingTime}, spawnerStarted: {monsterData.spawnerStarted}" );
        }
    }


    void Update()
    {
        if(!GameManager.instance.isLive){
            return;
        }

        for (int i = 0; i < spawnerMonsterArray.Length; i++)
        {
                if (!spawnerMonsterArray[i].spawnerStarted && GameManager.instance.gameTimer >= spawnerMonsterArray[i].startingTime){
                StartCoroutine(SpawnMonster(spawnerMonsterArray[i].monsterLevel));
                spawnerMonsterArray[i].spawnerStarted = true;
                
                Debug.Log($" ### Start Wave ### Wave: {spawnerMonsterArray[i].wavePhase}, Level: {spawnerMonsterArray[i].monsterLevel}, Time: {spawnerMonsterArray[i].startingTime}, spawnerStarted: {spawnerMonsterArray[i].spawnerStarted}, GameManager.instance.time: {GameManager.instance.gameTimer}" );
                
            }
        }

    }
    void Spawn(int monsterLevel)
    {
        int randomPointIndex = Random.Range(1, spawnPoints.Length); // 부모(Spawner)가 index가 0 이므로 빼줌 

        Transform spawnPoint = spawnPoints[randomPointIndex];
        GameObject monster = GameManager.instance.pool.Get(0);
        //Get(0) : Enemy Prefab, Get(1) : Bullet Prefab
         // prefab 을 하나만 만들고 애니메이터만 바꿔줌
        monster.transform.position = spawnPoint.position;
        monster.GetComponent<Enemy>().Init(monsterData[monsterLevel]);
    }


    // Coroutine to start spawning of moster
    IEnumerator SpawnMonster(int monsterLevel)
    {
        while(GameManager.instance.isLive){
            yield return new WaitForSeconds(frequencyOfSpawn);
            Spawn(monsterLevel);
        }

    }

}

