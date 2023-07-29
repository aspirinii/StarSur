using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet5Slash : Bullet
{
    // public new float damage;
    // public new int per;

    // private new bool isLive;
    // private new Rigidbody2D rigid; //상속받는걸로 처리 , 아님 그냥 주석 풀어줘도 됨 new 로 여기 변수로 작용 

    float timer; // weapon 1 fire timer 
    float stay = 0.2f;

    // Player player;
    // Vector3 offsetFromPlayer; 


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        // player = GameManager.instance.player;

    }

    private void OnEnable()
    {
        isLive = true;
        // this.transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    
    }
    public new void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        transform.localPosition = dir * 0.8f;
        // if(per >= 0){

        StartCoroutine(MoveBulletCoroutine(dir));
        // }

    }

    private IEnumerator MoveBulletCoroutine(Vector3 dir)
    {
        while (isLive)
        {
            transform.Translate(dir * 1f * Time.deltaTime);
            yield return null;  // Wait for the next frame
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
