using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    private GameManager gameManager;
    private MapManager mapManager;

    private int[] ratio = new int[System.Enum.GetValues(typeof(Item.Frequency)).Length];
    [SerializeField] public List<LootData> lootDataList = new List<LootData>();
    [SerializeField] public List<Item> itemList = new List<Item>();
    List<Item> itemListFrequency1 = new List<Item>();
    List<Item> itemListFrequency5 = new List<Item>();
    List<Item> itemListFrequency10 = new List<Item>();
    List<Item> itemListFrequency25 = new List<Item>();
    List<Item> itemListFrequency50 = new List<Item>();
    List<List<Item>> itemListFrequency = new List<List<Item>>(System.Enum.GetValues(typeof(Item.Frequency)).Length);

    public void SetUpSpawnManager(GameManager gameManager, MapManager mapManager) {
        if (gameManager == null || mapManager == null) {
            Debug.LogError("GameManager or MapManager is null!");
            return;
        }
        this.gameManager = gameManager;
        this.mapManager = mapManager;

        // Initialize the frequency-based item lists
        for (int i = 0; i < System.Enum.GetValues(typeof(Item.Frequency)).Length; i++) {
            itemListFrequency.Add(new List<Item>());
        }
    }

    public void OnStartGame() {
        GameObject[] lootableObjects = GameObject.FindGameObjectsWithTag("Lootable");

        foreach (GameObject lootableObject in lootableObjects) {
            LootData lootData = lootableObject.GetComponent<LootData>();

            if (lootData != null) {
                lootDataList.Add(lootData);
            }
        }

        GetRatio();
        DivideList();
        Spawn();
    }

    public void OldOnStartGame() {
        /*
        // For add to the "lootDataList" all the containers in the "ContainerList"
        Debug.Log("ContainerList" + TreasureRoom.ContainersList + TreasureRoom.ContainersList.Count);
        foreach (LootData lootData in TreasureRoom.ContainersList) {
            Debug.Log("ContainerList" + TreasureRoom.ContainersList + TreasureRoom.ContainersList.Count);
            lootDataList.Add(lootData);
        }
        */

        foreach (RoomBase room in mapManager.Rooms.Values) {

            if (room is TreasureRoom lootRoom) {
                lootDataList.Add(lootRoom.Chest);
            }
        }

        GetRatio();
        DivideList();
        Spawn();
    }

    void Start() {
       
    }

    private void GetRatio() {
        int i = 0;
        foreach (Item.Frequency frequencyType in System.Enum.GetValues(typeof(Item.Frequency))) {
            ratio[i] = (int) frequencyType;
            i++;
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
    public void Spawn() {
        foreach (LootData lootData in lootDataList) //moving in containers
        {
            int frequencyCount = lootData.frequencyCount.Length;

            for (int i = 0; i < frequencyCount; i++) //[0] = %1, [1] = %5, [2] = %10 ... //moving in probability
            {
                for (int j = 0; j < lootData.maxFrequencyCount[i]; j++) //moving in probability's count
                {
                    if (Random.Range(1, 100) <= ratio[i] && !lootData.isFull && lootData.frequencyCount[i] < lootData.maxFrequencyCount[i]) {
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
    private void SearchEmptyPlaceInMatrix(LootData lootData, ItemData itemData) {
        itemData.myLootContainer = lootData.transform.gameObject;

        int row = lootData.matrix.GetLength(0);
        int column = lootData.matrix.GetLength(1);
        Vector2Int itemSize = itemData.item.slotSize;

        for (int i = 0; i < row; i++) //moving in rows
        {
            for (int j = 0; j < column; j++) //moving in columns
            {
                if (i + itemSize.x <= row && j + itemSize.y <= column) //is there enough area. NO ROTATE
                {
                    bool status = false;
                    for (int x = 0; x < itemSize.x; x++) {
                        for (int y = 0; y < itemSize.y; y++) {
                            if (lootData.matrix[i + x, j + y] == true) // slot is full
                            {
                                status = true;
                            }
                        }
                    }
                    if (status == false) //avaible
                    {
                        AddItem(lootData, new Vector2Int(i, j), itemData);
                        return;
                    }
                }
                else if (i + itemSize.y <= row && j + itemSize.x <= column) //is there enough area. ROTATED
                {
                    bool status = false;

                    for (int x = 0; x < itemSize.y; x++) {
                        for (int y = 0; y < itemSize.x; y++) {
                            if (lootData.matrix[i + x, j + y] == true) {
                                status = true; // slot is full
                            }
                        }
                    }
                    if (status == false) //avaible
                    {
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
        lootData.itemList.Add(itemData); //spawned into container
        Vector2Int itemSize = itemData.item.slotSize;

        if (!itemData.isRotated) //if no rotated
        {
            for (int i = 0; i < itemSize.x; i++) {
                for (int j = 0; j < itemSize.y; j++) {
                    lootData.matrix[matrixPosition.x + i, matrixPosition.y + j] = true; // item's slots are made true
                }
            }
        }
        else //if rotated
        {
            for (int i = 0; i < itemSize.y; i++) {
                for (int j = 0; j < itemSize.x; j++) {
                    lootData.matrix[matrixPosition.x + i, matrixPosition.y + j] = true; // item's slots are made true
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