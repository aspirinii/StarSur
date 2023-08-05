using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    public int skillLevel; // 각각의 OItem (skill) 게임 오브젝트에서 각각의 스킬 데이터로 남게 된다. 
    public Weapon weapon;
    public Gear gear;

    Image icon;
    Text textLevel;
    Text textName;
    Text TextDescription;

    void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1]; // 자기자신이 [0] 이므로 child는 [1]로 접근
        icon.sprite = itemData.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        TextDescription = texts[2];
        textName.text = itemData.itemName;
    }

    private void OnEnable()
    {
        textLevel.text = "Lv." + (skillLevel+1);

        switch (itemData.itemType){
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                TextDescription.text = string.Format(itemData.itemDescription, itemData.damages[skillLevel], itemData.counts[skillLevel]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                TextDescription.text = string.Format(itemData.itemDescription, itemData.damages[skillLevel] * 100);
                break;
            case ItemData.ItemType.Heal:
                TextDescription.text = string.Format(itemData.itemDescription, itemData.baseDamage);
                break;
            case ItemData.ItemType.Slash:
                TextDescription.text = string.Format(itemData.itemDescription, itemData.damages[skillLevel]);
                break;
        }
            
    }

    public void OnClick()
    {
        switch(itemData.itemType){
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if( skillLevel == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else {
                    float nextDamage = CharacterStatus.Damage; //?
                    int nextCount = CharacterStatus.Count;

                    nextDamage += itemData.damages[skillLevel];
                    nextCount += itemData.counts[skillLevel];
                    weapon.LevelUp(nextDamage, nextCount);
                }
                skillLevel++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if (skillLevel == 0){
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(itemData);
                }
                else {
                    float nextRate = itemData.damages[skillLevel];
                    gear.LevelUp(nextRate);
                }
                skillLevel++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.health += (int)Math.Round(itemData.baseDamage);
                break;
            case ItemData.ItemType.Slash:
                if( skillLevel == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else{
                    float nextDamage = CharacterStatus.Damage;
                    int nextCount = CharacterStatus.Count; // 카운트는 의미 없지만 형태유지 
                    nextDamage += itemData.damages[skillLevel];
                    nextCount += itemData.counts[skillLevel];
                    weapon.LevelUp(nextDamage, nextCount);
                }
                skillLevel++;
                break;
            case ItemData.ItemType.Scarab:
                if( skillLevel == 0){
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(itemData);
                }
                else {
                    float nextDamage = CharacterStatus.Damage; //?
                    int nextCount = CharacterStatus.Count;
                    // 사실상 필요없는 할당 Null 대비?!

                    nextDamage += itemData.damages[skillLevel];
                    nextCount += itemData.counts[skillLevel];
                    weapon.LevelUp(nextDamage, nextCount);
                }
                skillLevel++;
                break;
        }



        if(skillLevel == itemData.damages.Length)
            GetComponent<Button>().interactable = false;

                        


    }


}
