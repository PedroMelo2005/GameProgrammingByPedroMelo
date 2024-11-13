using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject {
    [Header("Main Item Components")]
    public string ItemName;
    [Multiline] public string description;
    public int price;
    public int id;
    public Rarity rarity;
    public ITemType itemType;
    public SlotType slotType;
    public Frequency frequency;
    public Sprite image;
    public Color backgroundColor;
    public GameObject prefab;

    public enum Rarity {
        Common,
        Rare,
        Epic,
        Legendary
    }
    public enum ITemType {
        Weapon,
        Consumable,
        General
    }
    public enum SlotType {
        Weapon,
        Consumable,
        General
    }
    public enum Frequency {
        one = 1,
        five = 5,
        ten = 10,
        twenty_five = 25,
        fifty = 50
    }

}