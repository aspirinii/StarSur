using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float damage;
    public int per;

    protected bool isLive;
    protected Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        isLive = true;
    }
    public virtual void Init(float damage, int per, Vector3 dir)
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
