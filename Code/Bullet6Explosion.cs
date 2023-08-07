using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet6Explosion : Bullet
{
    // public float damage;
    // public int per;

    // protected bool isLive;
    // protected Rigidbody2D rigid;

    float timer = 0; // weapon 1 fire timer 
    float stay = 1f;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isLive = true;
    }
    public new void Init(float damage)
    {
        this.damage = damage;
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

    private void OnTriggerEnter2D(Collider2D other)
    {

    }



}
