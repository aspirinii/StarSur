using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;
    Text myText;
    Slider mySlider;
    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        if(!GameManager.instance.isLive)
            return;
        switch (type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                float nextExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / nextExp;
                break;
            case InfoType.Level:
                // myText.text = "LvLv " + GameManager.instance.level;
                myText.text = string.Format("Lv.{0:F0}",GameManager.instance.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}",GameManager.instance.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTimer;
                int min = Mathf.FloorToInt(remainTime / 60f);
                int sec = Mathf.FloorToInt(remainTime % 60f);
                myText.text = string.Format("{0:D2}:{1:D2}",min,sec);
                break;
            case InfoType.Health:
                float curhealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curhealth / maxHealth;
                break;
            default:
                break;
        }

    }

}
