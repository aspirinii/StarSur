using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // 특성 곱하는 옵션 기본 설정은 다른곳에서 
    // 여기는 캐릭터 초기값 설정 
    // 속성  : 함수나 변수를 선언할 때 사용하는 키워드
    public static float MoveSpeed
    {
        get{
            if (GameManager.instance.playerId == 0) {
                return 1.5f;
            } else if (GameManager.instance.playerId == 3) {
                return 5f;
            } else {
                return 1f;
            }
        }
        
    }

    public static float AttackSpeed
    {
        get{
            switch (GameManager.instance.playerId)
            {
                case 1:
                    return 1f;
                default:
                    return 1f;
            }
        }
    }

    public static float CircleSpeed
    {
        get{ return GameManager.instance.playerId == 1 ? 0.5f : 1f;}
    }

    public static float Damage
    {
        get{
            switch (GameManager.instance.playerId)
            {
                case 0:
                    return 10f;
                case 3:
                    return 20f;
                default:
                    return 5f;
            }
        }
    }

    public static int Count
    {
        get{ return GameManager.instance.playerId == 2 ? 3 : 1;}
    }
}
