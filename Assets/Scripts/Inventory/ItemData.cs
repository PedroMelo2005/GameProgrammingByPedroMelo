using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData {

    public ItemData(string itemName, Rarity rarity) {
        ItemName = itemName;
        ItemRarity = rarity;
    }

    public string ItemName;
    public Rarity ItemRarity;

}