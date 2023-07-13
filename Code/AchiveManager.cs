using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;

    enum Achive {UnlockOrange, UnlockBlueberry }
    Achive[] achives;
    WaitForSecondsRealtime wait;

    private void Awake()
    {
        // achives = Enum.GetValues(typeof(Achive)) as Achive[];
        //transform Enum to Array
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(5f);
        if (!PlayerPrefs.HasKey("Started"))
        {
            Init();
        }

    }

    void Init()
    {
        // save data in hdware
        PlayerPrefs.SetInt("Started", 1);

        foreach (Achive achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }


    }

    private void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        foreach (Achive achive in achives)
        {
            if (PlayerPrefs.GetInt(achive.ToString()) == 1)
            {
                switch (achive)
                {
                    case Achive.UnlockOrange:
                        lockCharacter[0].SetActive(false);
                        unlockCharacter[0].SetActive(true);
                        break;
                    case Achive.UnlockBlueberry:
                        lockCharacter[1].SetActive(false);
                        unlockCharacter[1].SetActive(true);
                        break;
                }
            }
        }
    }

    void LateUpdate()
    {
        foreach (Achive achive in achives){
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        if (PlayerPrefs.GetInt(achive.ToString()) == 1)
            return;
        bool achiveDone = false;
        switch (achive)
        {
            case Achive.UnlockOrange:
                if (GameManager.instance.kill >= 10)
                {
                    achiveDone = true;
                }
                break;
            case Achive.UnlockBlueberry:
                if (GameManager.instance.gameTimer == GameManager.instance.maxGameTime && GameManager.instance.kill >= 1)
                {
                    achiveDone = true;
                }
                break;
    
        }
        if (achiveDone)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
            for (int i = 0; i < achives.Length; i++)
            {
                if (achives[i] == achive)
                {
                    lockCharacter[i].SetActive(false);
                    unlockCharacter[i].SetActive(true);
                    uiNotice.transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            StartCoroutine(NoticeRoutine());
         
        }
    }
    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        yield return wait;
        uiNotice.SetActive(false);
    }

}
