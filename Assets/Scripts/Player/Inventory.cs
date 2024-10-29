using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    public List<Item> InventoryList = new List<Item>();

    // Method that return a random item of itemList
    Item RandomItem() {
        // Create a List that has probability of drop items
        List<Item> itemList = new List<Item>();

        // For loop that add 20 times "new HealPotion()" 20% of chance to be dropped
        for (int i = 0; i < 20; i++) {
            itemList.Add(new HealPotion());
        }

        // For loop that add 30 times "new Dagger()" 30% of chance to be dropped
        for (int i = 0; i < 30; i++) {
            itemList.Add(new Dagger());
        }

        // For loop that add 30 times "new LongSword()" 30% of chance to be dropped
        for (int i = 0; i < 30; i++) {
            itemList.Add(new LongSword());
        }

        // For loop that add 15 times "new LightSaber()" 15% of chance to be dropped
        for (int i = 0; i < 15; i++) {
            itemList.Add(new Spear());
        }

        // For loop that add 5 times "new SupermanPower()" 5% of chance to be dropped
        for (int i = 0; i < 5; i++) {
            itemList.Add(new Axe());
        }

        return itemList[Random.Range(0, itemList.Count)]; // return a random item of itemList
    }

    // Function AddItemToInventory that add item to the inventory
    public void AddItemToInventory(ref string itemFound) {
        Item item = RandomItem(); // Assign the RandomItem() to local variable "item"
        InventoryList.Add(item); // Add RandomItem() to InventoryList
        itemFound = item.ItemName; // Assign RandomItem().ItemName to itemFound
    }

    // Function RemoveItemOfInventory that remove item from the inventory
    public void RemoveItemOfInventory(Item item) {
        InventoryList.Remove(item);
    }

    // Function CheckInventory that check the inventory
    public void CheckInventory() {
        // If InventoryList has one item or more run the code
        if (InventoryList.Count > 0) {
            Debug.Log("Your inventory has:");
            // For loop that is execute for each item in InventoryList
            foreach (Item item in InventoryList) {
                Debug.Log(item.ItemName);
            }
        }
        else {
            Debug.Log("Your inventory is empty");
        }
    }

    // Function GetWeapon that add items of type weapon to the weaponList
    public List<Weapon> GetWeapons() {
        List<Weapon> weaponsList = new List<Weapon>(); // Create List of weaponsList
                                                       // For each item in the InventoryList will run the code
        foreach (Item item in InventoryList) {
            // If the item is from Weapon class will be added to the weaponList
            if (item is Weapon weapon) {
                weaponsList.Add(weapon);
            }
        }
        return weaponsList;
    }

    // Function GetConsumables that add items of type consumables to the consumablesList
    public List<Consumable> GetConsumables() {
        List<Consumable> consumablesList = new List<Consumable>(); // Create List of consumablesList
                                                                   // For each item in the InventoryList will run the code
        foreach (Item item in InventoryList) {
            // If the item is from Consumable class will be added to the consumablesList
            if (item is Consumable consumable) {
                consumablesList.Add(consumable);
            }
        }
        return consumablesList;
    }

}