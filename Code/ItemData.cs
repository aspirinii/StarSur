using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Melee, //0
        Range, //1
        Glove, //2
        Shoe, //3
        Heal,   //4
        Slash, //5
        Scarab, //6
        Stab, //7
        CircleSlash //8
    }

    [Header("# Main Information")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]//윗줄에 작성 
    public string itemDescription;

    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite handSprite;
}
