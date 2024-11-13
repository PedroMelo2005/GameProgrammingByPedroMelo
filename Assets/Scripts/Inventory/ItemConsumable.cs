using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Item/NewConsumable")]
public class ItemConsumable : Item {
    [Header("Consumable Components")]
    public int heal;
}