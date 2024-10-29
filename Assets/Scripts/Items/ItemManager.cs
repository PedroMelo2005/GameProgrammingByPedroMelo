using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    public static Item itemInstance;
    private Item _item;

    [SerializeField] LongSword longSwordPrefab;
    [SerializeField] Dagger daggerPrefab;
    [SerializeField] Spear spearPrefab;
    [SerializeField] Axe axePrefab;
    [SerializeField] HealPotion healPotionPrefab;
}