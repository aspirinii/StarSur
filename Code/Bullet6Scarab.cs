using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet6Scarab : Bullet
{

    // public float damage;
    // public int per;
    // protected bool isLive;
    // protected Rigidbody2D rigid;
    public GameObject explosionObject; 
    public int prefabPoolexplosionId;
    // call explosion object from pool manager
    // call explosion object from pool manager
    readonly int explosionDamage =100;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isLive = true;
    }
    public void Init(float damage, Vector3 dir) 
    // per is not use so it is not need to override Init
    {
        this.damage = damage;

        rigid.velocity = dir* 15f;

    }
    public void SetExplosionPrefabPoolId(int id){
        prefabPoolexplosionId = id;
    }



    private void OnTriggerEnter2D(Collider2D other)//it is not work
    {
        if(!isLive)
            return;

        if(!other.CompareTag("Enemy") || per == -100){
            return;
        }

        isLive = false;
        // make explosion object from pool manager
        // 1. get explosion object from pool manager
        // 2. set position and rotation
        // 3. set damage
        // 4. play sound
        // 5. set active false
        Transform explosion = GameManager.instance.pool.Get(prefabPoolexplosionId).transform;
        explosion.SetPositionAndRotation(transform.position, Quaternion.identity);
        // GameObject explosion = Instantiate(explosionObject, transform.position, Quaternion.identity);

        explosion.GetComponent<Bullet6Explosion>().Init(explosionDamage); 
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        gameObject.SetActive(false);
    }



}

