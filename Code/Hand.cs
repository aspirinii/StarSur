using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool melee;
    public SpriteRenderer spriteRenderer;

    SpriteRenderer playerSpriteRenderer;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    void Awake()
    {
        // playerSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        playerSpriteRenderer = GetComponentsInParent<SpriteRenderer>()[1]; // 부모의 sprite renderer
    }

    private void LateUpdate()
    {
        bool isFlip = playerSpriteRenderer.flipX;

        if(melee){ // 근접무기
            transform.localRotation = isFlip ? leftRotReverse : leftRot;
            spriteRenderer.flipY = isFlip;
            spriteRenderer.sortingOrder = isFlip ? 4 : 6;
        }else{ // 원거리무기 
            transform.localPosition = isFlip ? rightPos : rightPosReverse;
            spriteRenderer.flipX = isFlip;
            spriteRenderer.sortingOrder = isFlip ? 6 : 4;

        }
    }

}
