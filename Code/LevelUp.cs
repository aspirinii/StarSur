using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show()
    {
        Next();
        // gameObject.SetActive(true); // 시작할때 액티브 되어 있어야.. 초기 레벨업하여 진행 가능.. 현재는 초기 시작이 레벨업 방식이라 안됨.. 초기 레벨설정을 UI 방식이 아닌 다른방식으로 해야 Set false 해둘수 있음 .
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);

    }
    public void Hide()
    {
        // gameObject.SetActive(false);
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);

    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        //1.모든아이템비활성화
        foreach(Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        //2.그 중 랜덤 3개 활성화
        int[] ran = new int[3];
        while (true) {
            ran[0] = Random.Range(0, items.Length);
            ran[1] = Random.Range(0, items.Length);
            ran[2] = Random.Range(0, items.Length);

            if(ran[0] != ran[1] && ran[0] != ran[2] && ran[1] != ran[2])
            {
                break;
            }
        }

        for (int index=0; index < ran.Length; index++)
        {
            Item ranItem = items[ran[index]];
            if (ranItem.skillLevel == ranItem.itemData.damages.Length )
            {
                items[4].gameObject.SetActive(true);

            }

            else{
            items[ran[index]].gameObject.SetActive(true);
            }
        }
        //3.만렙아이템은 소비아이템으로 대체


    }
}
