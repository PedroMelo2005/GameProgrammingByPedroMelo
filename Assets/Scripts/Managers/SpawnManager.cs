using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour {
    private int[] ratio = new int[System.Enum.GetValues(typeof(Item.Frequency)).Length];
    [SerializeField] public List<LootData> lootDataList = new List<LootData>();
    [SerializeField] public List<Item> itemList = new List<Item>();
    List<Item> itemListFrequency1 = new List<Item>();
    List<Item> itemListFrequency5 = new List<Item>();
    List<Item> itemListFrequency10 = new List<Item>();
    List<Item> itemListFrequency25 = new List<Item>();
    List<Item> itemListFrequency50 = new List<Item>();
    List<List<Item>> itemListFrequency = new List<List<Item>>(System.Enum.GetValues(typeof(Item.Frequency)).Length);

    public void SetUp(MapManager mapManager) {

        foreach (RoomBase room in mapManager.Rooms.Values) {

            if (room is TreasureRoom lootRoom) {
                lootDataList.Add(lootRoom.Chest);
            }
        }

        GetRatio();
        DivideList();
        Spawn();
    }

    private void GetRatio() {
        int i = 0;

        foreach (Item.Frequency frequencyType in System.Enum.GetValues(typeof(Item.Frequency))) {
            ratio[i] = (int)frequencyType;
            i++;
        }
    }

    public void Spawn() {

        // Moving in containers
        foreach (LootData lootData in lootDataList) {
            int frequencyCount = lootData.frequencyCount.Length;

             // [0] = 1%, [1] = 5%, [2] = 10%, [3] = 25%, [4] = 50%
             // Moving in probability
            for (int i = 0; i < frequencyCount; i++) {

                // Moving in probability count
                for (int j = 0; j < lootData.maxFrequencyCount[i]; j++) {

                    if (Random.Range(1, 100) <= ratio[i] && !lootData.isFull && lootData.frequencyCount[i] <= lootData.maxFrequencyCount[i]) {
                        int random = Random.Range(0, itemListFrequency[i].Count);
                        ItemData newItemData = new ItemData();
                        newItemData.item = itemListFrequency[i][random];
                        newItemData.slotPanelType = InventoryPanel.Type.Loot;
                        SearchEmptyPlaceInMatrix(lootData, newItemData);
                    }
                }
            }
        }
    }
    
    private void DivideList() {
        itemListFrequency.Add(itemListFrequency1);
        itemListFrequency.Add(itemListFrequency5);
        itemListFrequency.Add(itemListFrequency10);
        itemListFrequency.Add(itemListFrequency25);
        itemListFrequency.Add(itemListFrequency50);

        foreach (Item item in itemList) {

            switch (item.frequency) {

                case Item.Frequency.one: {
                    itemListFrequency1.Add(item);
                    break;
                }

                case Item.Frequency.five: {
                    itemListFrequency5.Add(item);
                    break;
                }

                case Item.Frequency.ten: {
                    itemListFrequency10.Add(item);
                    break;
                }

                case Item.Frequency.twenty_five: {
                    itemListFrequency25.Add(item);
                    break;
                }

                case Item.Frequency.fifty: {
                    itemListFrequency50.Add(item);
                    break;
                }
            }
        }
    }

    private void SearchEmptyPlaceInMatrix(LootData lootData, ItemData itemData) {
        itemData.myLootContainer = lootData.transform.gameObject;

        int row = lootData.matrix.GetLength(0);
        int column = lootData.matrix.GetLength(1);
        Vector2Int itemSize = itemData.item.slotSize;

        // Moving in rows
        for (int i = 0; i < row; i ++) {
            
            // Moving in columns
            for (int j = 0; j < column; j++) {

                if (i + itemSize.x <= row && j + itemSize.y <= column) {
                    bool status = false;

                    for (int x = 0; x < itemSize.x; x++) {

                        for (int y = 0; y < itemSize.y; y++) {

                            // Slot is full
                            if (lootData.matrix[i + x, j + y] == true) {
                                status = true;
                            }
                        }
                    }

                    // Available
                    if (status == true) {
                        AddItem(lootData, new Vector2Int(i, j), itemData);
                        return;
                    }
                }
                else if (i + itemSize.y <= row && j + itemSize.x <= column) {
                    bool status = false;

                    for (int x = 0; x < itemSize.y; x++) {

                        for (int y = 0; y < itemSize.x; y++) {

                            // Slot is full
                            if (lootData.matrix[i + x, j + y] == true) {
                                status = true;
                            }
                        }
                    }

                    // Available
                    if (status == false) {
                        itemData.isRotated = true;
                        AddItem(lootData, new Vector2Int(i, j), itemData);
                        return;
                    }
                }
            }
        }
    }

    private void AddItem(LootData lootData, Vector2Int matrixPosition, ItemData itemData) {
        itemData.matrixPosition = matrixPosition;
        lootData.itemList.Add(itemData); // Spawned into container
        Vector2Int itemSize = itemData.item.slotSize;

        // If is not rotated
        if (itemData.isRotated == false) {

            for (int i = 0; i < itemSize.x; i++) {

                for (int j = 0; j < itemSize.y; j++) {
                    lootData.matrix[matrixPosition.x + i, matrixPosition.y + j] = true; // Item slot are made true

                }
            }
        }
        // If is rotated
        else {

            for (int i = 0; i < itemSize.y; i++) {

                for (int j = 0; j < itemSize.x; j++) {
                    lootData.matrix[matrixPosition.x + i, matrixPosition.y + j] = true; // Item slot are made true
                }
            }
        }

        int f = 0;

        foreach (Item.Frequency frequencyType in System.Enum.GetValues(typeof(Item.Frequency))) {

            if (frequencyType == itemData.item.frequency) {
                lootData.frequencyCount[f] += 1;
                break;
            }
            f++;
        }
    }

}