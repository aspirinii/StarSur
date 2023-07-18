using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet5Slash : Bullet
{
    // public new float damage;
    bool isLive;
    // public new int per;

    Rigidbody2D rigid;

    float timer; // weapon 1 fire timer 
    float stay = 0.3f;


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
            rigid.velocity = dir* 1f;
        }
    }
    private void FixedUpdate()
    {
        if(!isLive)
            return;

        timer += Time.deltaTime;
        if(timer > stay){
            timer = 0;
            gameObject.SetActive(false);
        }

        
    }


    private void OnTriggerEnter2D(Collider2D other)//it is not work
    {
        if(!isLive)
            return;


        if(!other.CompareTag("Enemy") || per == -100){

            return;
        }
        per--;

        if(per < 0)
        {
            isLive = false;
            // rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area") || per == -100)
            return;
        gameObject.SetActive(false);
    }

}
