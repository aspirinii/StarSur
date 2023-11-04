using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D collid;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;


        switch (transform.tag) {
            case "Ground":
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;
                float dirX = diffX < 0 ?  -1 : 1; 
                float dirY = diffY < 0 ?  -1 : 1; 
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);
                // Debug.Log("PlayerPos : " + playerPos);
                // Debug.Log("MyPos : " + myPos);

                if(diffX > diffY){
                    transform.position = new Vector3(myPos.x + dirX * 40, myPos.y, myPos.z);
                } else {
                    transform.position = new Vector3(myPos.x, myPos.y + dirY * 40, myPos.z);
                }
                break;
            case "Enemy":
                if(collid.enabled){
                    Vector3 dist = playerPos - myPos;
                    Vector3 ranRange = new Vector3(Random.Range(0,5), Random.Range(0,5), 0);
                    transform.Translate(ranRange+dist*2);
                }

                break;

        }


    }
}
