using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet6Explosion : Bullet
{
    // public float damage;
    // float per = 100;

    // protected bool isLive;
    // protected Rigidbody2D rigid;

    float timer = 0; // weapon 1 fire timer 
    readonly float stay = 1f;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isLive = true;
    }
    public void Init(float damage)
    {
        this.damage = damage;
        this.per = 100;
    }
    private void FixedUpdate()
    {


        if(!isLive)
            return;

        timer += Time.deltaTime;
        if(timer > stay){
            timer = 0;
            gameObject.SetActive(false);
            isLive = false;
        }

        
    }



}
