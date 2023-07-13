using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // 특성 곱하는 옵션 기본 설정은 다른곳에서 
    // 속성  : 함수나 변수를 선언할 때 사용하는 키워드
    public static float MoveSpeed
    {
        get{
            if (GameManager.instance.playerId == 0) {
                return 1.5f;
            } else if (GameManager.instance.playerId == 3) {
                return 30f;
            } else {
                return 1f;
            }
        }
        
    }

    public static float AttackSpeed
    {
        get{ return GameManager.instance.playerId == 1 ? 2f : 1f;}
    }

    public static float CircleSpeed
    {
        get{ return GameManager.instance.playerId == 1 ? 0.5f : 1f;}
    }

    public static float Damage
    {
        get{
            if (GameManager.instance.playerId == 0) {
                return 1.5f;
            } else if (GameManager.instance.playerId == 3) {
                return 10f;
            } else {
                return 1f;
            }
        }
    }

    public static int Count
    {
        get{ return GameManager.instance.playerId == 2 ? 3 : 1;}
    }
}
