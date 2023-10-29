using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCircleSlash : Bullet5Slash // Bullet을 상속받은 Bullet5Slash 상속 받기
{
    
    private float timer; // weapon 1 fire timer 
    private readonly float stay = 2f;
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
    
    }
    public void Init(float damage)
    {
        this.damage = damage;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

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
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 일방적인 상속 방지 클래스
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area") || per == -100)
            return;
        gameObject.SetActive(false);
    }   
}
