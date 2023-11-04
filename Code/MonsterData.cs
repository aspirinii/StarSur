using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Monster", menuName = "Scriptable Object/MonsterData")]
public class MonsterData : ScriptableObject
{
    public float spawnTime;
    //몇초당 1개 몬스터 생성되는지 
    public int spriteType;
    // spriteType
    public int health;
    public float speed;
    public int damage;

}