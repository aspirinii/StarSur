using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // bullet manager claas 

    public int id;
    public int prefabPoolId;
    public float damage;
    public int count; // bullet number , peierce 
    public float speed; // attack speed , rotation speed , change it someday
    float timer; // weapon 1 fire timer 
    Player player;

    private void Awake()
    {
        player = GameManager.instance.player;
    }


    void Update()
    {
        if(!GameManager.instance.isLive)
            return;
        switch (id) {
            case 0 :
                // transform.Rotate(0,0,speed * Time.deltaTime);
                transform.Rotate(Vector3.back * speed * Time.deltaTime); // 한프레임이 소비하는시간 DeltaTime
                break;

            case 1 :
                timer += Time.deltaTime;
                if(timer > speed){
                    timer = 0;
                    Fire();
                }
                break;

            case 5 :
                timer += Time.deltaTime;
                if(timer > speed){
                    timer = 0;
                    Slash();
                }
                break;
            
            case 6 :
                timer += Time.deltaTime;
                if(timer > speed){
                    timer = 0;
                    Scarab();
                }
                break;

            default:
                break;
        }

        //test code
        // if(Input.GetButtonDown("Jump")){
        // //     LevelUp(10,1);
        // }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage += damage;
        this.count = count;

        if(id == 0)
            Batch();

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // player에 있는 ApplyGear 함수를 호출
    }

    public void Init(ItemData data)
    {
        // Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        // Property Set
        id = data.itemId;
        damage = data.baseDamage;
        count = data.baseCount + CharacterStatus.Count;

        for (int index = 0; index < GameManager.instance.pool.prefabs.Length; index++) // 생성
        {
            if (data.projectile == GameManager.instance.pool.prefabs[index])
            {
                prefabPoolId = index;
                break;
            }
        }
        // Attackspeed; make bullet
        switch (id) {
            case 0 :
                speed = 150 * CharacterStatus.CircleSpeed;
                Batch();
                break;
            case 1 : 
                speed = 1f * CharacterStatus.AttackSpeed;
                break;
            case 5 :
                speed = 1f * CharacterStatus.AttackSpeed;
                break;
            case 6 :
                speed = 1f * CharacterStatus.AttackSpeed;
                break;
            default:
                break;
        }
        //hand set 
        switch (id) {
            case 0 :
            case 1 :
                Hand hand = player.hands[(int)data.itemType];
                hand.spriteRenderer.sprite = data.handSprite;
                hand.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        //Broadcast 나중에 뭔지 더 확인하기  
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver); // player에 있는 ApplyGear 함수를 호출
    }

    void Batch()
    {
        for (int index=0; index < count; index++){
            Transform bullet;

            if(index < transform.childCount){
                bullet = transform.GetChild(index); // 자식에 돌고있는 위치(인덱스)를 가져옴
            }
            else{ // 불렛을 추가하는 코드
                bullet = GameManager.instance.pool.Get(prefabPoolId).transform;
                bullet.parent = transform; // 불렛의 부모를 웨폰 자기자신으로 설정
            }

            bullet.parent = transform;
            bullet.localPosition = new Vector3(0,0,0);
            bullet.localRotation = Quaternion.identity;
            Vector3 rotVec = Vector3.forward * (360f / count) * index;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero); // -100 is Infinity Per.(관통)
        }
    }

    void Fire()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabPoolId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // bullet.rotation = Quaternion.identity;
        bullet.GetComponent<Bullet>().Init(damage, count, dir); // 1 is 1 Per.
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void Slash() // 조금더 수정 필요 
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabPoolId).transform;
        bullet.position = player.transform.position;

        bullet.SetParent(transform);
        // bullet.localPosition = dir*0.5f;
     
        float tan = Mathf.Atan2(dir.y, dir.x);
        float rotationOfZ = (tan * Mathf.Rad2Deg);
        bullet.rotation = Quaternion.Euler(0,0,rotationOfZ);
        
        bullet.GetComponent<Bullet5Slash>().Init(damage, count, dir); // 1 is 1 Per.
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }

    void Scarab()
    {
        if(!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabPoolId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        // bullet.rotation = Quaternion.identity;
        bullet.GetComponent<Bullet6Scarab>().Init(damage, count, dir); // 1 is 1 Per.
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
