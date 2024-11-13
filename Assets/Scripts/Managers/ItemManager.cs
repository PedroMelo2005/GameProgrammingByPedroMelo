using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {
    [SerializeField] Dagger daggerPrefab;
    [SerializeField] LongSword longSwordPrefab;
    [SerializeField] Spear spearPrefab;
    [SerializeField] Axe axePrefab;
    [SerializeField] HealPotion healPotionPrefab;

    // Create the Instances
    public static ItemManager Instance; // Global Static "Instance" instance of type "ItemManager"
    private ItemScript _itemScript; // Private "_item" instance of type "Item"

    public void SetUpItemManager() {
        Debug.Log("SetUp the \"ItemManager\""); // DEBUG
    }

    // Method that return a random item of "itemList"
    public ItemScript RandomItem() {
        // Create a List that has probability of drop items
        List<ItemScript> itemList = new List<ItemScript>();

        // For loop that add 20 times "healPotionPrefab" 20% of chance to be dropped
        for (int i = 0; i < 20; i++) {
            itemList.Add(healPotionPrefab);
        }

        // For loop that add 30 times "daggerPrefab" 30% of chance to be dropped
        for (int i = 0; i < 30; i++) {
            itemList.Add(daggerPrefab);
        }

        // For loop that add 30 times "longSwordPrefab" 30% of chance to be dropped
        for (int i = 0; i < 30; i++) {
            itemList.Add(longSwordPrefab);
        }

        // For loop that add 15 times "spearPrefab" 15% of chance to be dropped
        for (int i = 0; i < 15; i++) {
            itemList.Add(spearPrefab);
        }

        // For loop that add 5 times "axePrefab" 5% of chance to be dropped
        for (int i = 0; i < 5; i++) {
            itemList.Add(axePrefab);
        }

        return itemList[Random.Range(0, itemList.Count)]; // return a random item of "itemList"
    }
}