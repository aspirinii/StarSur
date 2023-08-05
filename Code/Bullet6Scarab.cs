using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet6Scarab : Bullet
{

    // public float damage;
    // public int per;

    // protected bool isLive;
    // protected Rigidbody2D rigid;
    private int prefabPoolId;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isLive = true;
    }
    public new void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if(per >= 0){
            rigid.velocity = dir* 15f;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)//it is not work
    {
        if(!isLive)
            return;


        if(!other.CompareTag("Enemy") || per == -100){

            return;
        }

        isLive = false;


        // Instantiate second explosion object at the current transform position from pool manager
        Transform explosion = GameManager.instance.pool.Get(prefabPoolId).transform;


        gameObject.SetActive(false);
        


    }



}

