using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsStab : Bullet5Slash // Bullet을 상속받은 Bullet5Slash 상속 받기
{
    
    private float timer; // weapon 1 fire timer 
    private readonly float stay = 1f;
    // private void Awake()
    // {
    //     // rigid = GetComponent<Rigidbody2D>();
    //     // player = GameManager.instance.player;

    // }

    private void OnEnable()
    {

        isLive = true;
        // this.transform.position = Vector3.zero;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.Rotate(0, 0, -90);
    
    }
    public new void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        transform.localPosition = dir * 0.8f;
        // if(per >= 0){
        float angleDegrees = 90.0f;
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        Vector3 dir90 = new(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0 );
        dir90.Normalize();


        StartCoroutine(MoveBulletCoroutine(dir90));
        // }

    }

    private IEnumerator MoveBulletCoroutine(Vector3 dir)
    {
        while (isLive)
        {
            transform.Translate(1f * Time.deltaTime * dir);
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
            isLive = false;
            gameObject.SetActive(false);
        }

        
    }


    private void OnTriggerEnter2D(Collider2D other)//it is not work
    {
        // if(!isLive)
        //     return;


        // if(!other.CompareTag("Enemy") || per == -100){

        //     return;
        // }
        // per--;

        // if(per < 0)
        // {
        //     isLive = false;
        //     // rigid.velocity = Vector2.zero;
        //     gameObject.SetActive(false);
        // }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area") || per == -100)
            return;
        isLive = false;
        gameObject.SetActive(false);    }   
}
