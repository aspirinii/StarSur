using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType itemType;
    public float rate;
    public void Init(ItemData itemData)
    {
        // basic set
        name = "Gear" + itemData.itemId;
        transform.parent = GameManager.instance.player.transform;

        // property set
        itemType = itemData.itemType;
        rate = itemData.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(itemType){
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
            case ItemData.ItemType.Heal:
                break;
        }
    }

    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in weapons)
            switch(weapon.id){
                case 0:
                    float circleSpeed = 150 * CharacterStatus.CircleSpeed;
                    weapon.speed = circleSpeed*( 1f + rate);
                    break;
                case 1:
                    float attackSpeed = 1f * CharacterStatus.AttackSpeed;
                    weapon.speed = attackSpeed * (1f - rate);
                    break;
                default:
                    break;
            }    
    }
    void SpeedUp()
    {
        float speed = 3 * CharacterStatus.MoveSpeed;
        GameManager.instance.player.moveSpeed = speed + (speed * rate);
    }

}
