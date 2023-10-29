using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerDirectionAngle : MonoBehaviour
{
    public static PlayerDirectionAngle instance;
    private Vector2 nonZeroInput;
    private Vector2 InputJustBefore = new Vector2(0,0);
    private Vector2 inputVec;
    public float InputAngleRad;
    private float TriangleMoveSpeed = 7f;
    public Transform playerTransform;

    public Vector2 InputAngleVector2{
        get{
            return InputJustBefore;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {

        if(!GameManager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");// 미끄러지지않고 Raw 
        inputVec.y = Input.GetAxisRaw("Vertical");

// 0입력없을 경우 방향 유지 
        if(inputVec.x == 0 && inputVec.y == 0){
            nonZeroInput = InputJustBefore;
        }else{
            InputJustBefore= inputVec;
            nonZeroInput= inputVec;
        }

        InputAngleRad = Mathf.Atan2(nonZeroInput.y, nonZeroInput.x);
        float InputAngleDeg = InputAngleRad * Mathf.Rad2Deg;
        float upwardSpriteRotation = InputAngleDeg-90;
        Vector3 offsetFromPlayer = new Vector3(Mathf.Cos(InputAngleRad), Mathf.Sin(InputAngleRad), 0) * 0.7f;

        
        // triangle
        transform.parent.localPosition = Vector3.Lerp(transform.parent.localPosition, new Vector3(playerTransform.position.x, playerTransform.position.y,0) + offsetFromPlayer, Time.deltaTime*TriangleMoveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,upwardSpriteRotation), Time.deltaTime*TriangleMoveSpeed);
        // transform.parent.localPosition = playerTransform.position + offsetFromPlayer;
        // transform.rotation = Quaternion.Euler(0,0,upwardSpriteRotation);



    }
}
