using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParentBackpack;
    [SerializeField] private Transform slotParentLoot;

    public Transform ItemParentCharacter;
    public Transform ItemParentBackpack;
    public Transform ItemParentLoot;

    // Items UI Prefabs
    [SerializeField] private GameObject item1x1;
    [SerializeField] private GameObject item1x2;
    [SerializeField] private GameObject item1x3;
    [SerializeField] private GameObject item1x4;
    [SerializeField] private GameObject item1x5;
    [SerializeField] private GameObject item2x2;

    // Dragging
    public bool isDraggingItem;
    public GameObject draggingItem;
    public float draggingItemSmoothFactor = 10f;
    public Transform draggingItemParent;

    // Color
    public Color agree;
    public Color disagree;

    // Panel
    public List<InventoryPanel> panelList = new List<InventoryPanel>();

    // 
    public bool isLockedUI;
    private Transform lastsSlot;

    private static InventoryManager instance;
    public static InventoryManager Instance;

    void Awake() {
        Instance = this;
    }

    void Start () {
        panelList = GetComponents<InventoryPanel>().ToList();
    }

    void Update() {

    }

    public IEnumerator CreatePanel(InventoryPanel panel, LootData lootData) {
        ClearPanel(panel.type);

        // For lootData and lootPanel
        if (lootData != null) {
            panel.size = lootData.size;
        }
        yield return new WaitForEndOfFrame();

        // Creating slots
        for (int i = 0; i <panel.size.x; i++) {

            for (int j = 0; j < panel.size.y; j++) {
                GameObject slot = Instantiate(slotPrefab, panel.slotParent);
                slot.transform.GetComponent<SlotData>().matrixPosition = new Vector2Int(i, j);
                slot.transform.GetComponent<SlotData>().inventoryPanelType = panel.type;

                if (lootData !=  null) {
                    slot.transform.GetComponent<SlotData>().myLootContainer = lootData.gameObject;
                }
            }
        }
        yield return new WaitForEndOfFrame();
        panel.matrix = new bool[panel.size.x, panel.size.y];

        // Add items
        if (lootData != null) {
            FindSlotPositionForItem(panel.slotParent, lootData.itemList);
            panel.itemDataList = lootData.itemList; // List synced

        }
        else {
            FindSlotPositionForItem(panel.slotParent, lootData.itemList);
        }
        FillItem(panel, lootData);
    }

    public void ClearPanel(InventoryPanel.Type panelType) {
        InventoryPanel panel = GetPanel(panelType);

        // Removing Slots
        foreach (Transform slot in panel.slotParent) {
            Destroy(slot.gameObject);
        }

        // Removing Items
        foreach (Transform item in panel.itemParent) {
            Destroy(item.gameObject);
        }
    }

    public void FindSlotPositionForItem(Transform slotParent, List<ItemData> itemDataList) {

        foreach (ItemData itemData in itemDataList) {

            foreach (Transform slot in slotParent) {

                if (slot.GetComponent<SlotData>().matrixPosition == itemData.matrixPosition) {
                    itemData.slotPosition = slot.GetComponent<RectTransform>().position;
                    break;
                }
            }
        }
    }

    public void FillItem(InventoryPanel panel, LootData lootData) {

        // For lootPanel
        if (lootData != null) {

            foreach (ItemData itemData in lootData.itemList) {
                GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                newItem.transform.GetComponent<ItemUI>().Initialize();
                SetMatrixThanPanel(itemData, true);
            }
        }
        // For other panels
        else {

            if (panel.type == InventoryPanel.Type.Backpack) {

                // 
                foreach (ItemData itemData in lootData.itemList) {
                    GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                    newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                    newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                    newItem.transform.GetComponent<ItemUI>().Initialize();
                    SetMatrixThanPanel(itemData, true);
                }
            }
            else if (panel.type == InventoryPanel.Type.Character) {

                FindSlotPositionForItemCharacterPanel(panel.itemDataList);
                foreach (ItemData itemData in lootData.itemList) {
                    GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                    newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                    newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                    newItem.transform.GetComponent<ItemUI>().Initialize();
                    SetMatrixThanPanel(itemData, true);
                }
            }
        }
    }

    private GameObject GetMyPrefab(Vector2Int size) {

        if (size == new Vector2Int(1,1)) {
            return item1x1;
        }
        else if (size == new Vector2Int(1, 2)) {
            return item1x2;
        }
        else if (size == new Vector2Int(1, 3)) {
            return item1x3;
        }
        else if (size == new Vector2Int(1, 4)) {
            return item1x4;
        }
        else if (size == new Vector2Int(1, 5)) {
            return item1x5;
        }
        else if (size == new Vector2Int(2, 2)) {
            return item2x2;
        }
        return null;
    }

    private void SetMatrixThanPanel(ItemData itemData, bool value) {
        InventoryPanel panel = GetPanel(itemData.slotPanelType);
        Vector2Int itemSize = itemData.item.slotSize;

        // For the Character Panel
        if (itemData.slotPanelType == InventoryPanel.Type.Character) {

            foreach (Transform slot in panel.slotParent) {

                if (slot.GetComponent<SlotData>().matrixPosition.y == itemData.matrixPosition.y) {
                    slot.GetComponent<SlotData>().isFull = value;
                    return;
                }
            }
        }

        // For other panels
        if (!itemData.isRotated) { // If NOT rotated

            for (int i = 0; i < itemSize.x; i++) {

                for (int j = 0; j < itemSize.y; j++) {
                    panel.matrix[itemData.matrixPosition.x + i, itemData.matrixPosition.y + j] = value;

                }
            }
        }
        else { // If rotated

            for (int i = 0; i < itemSize.y; i++) {

                for (int j = 0; j < itemSize.x; j++) {
                    panel.matrix[itemData.matrixPosition.x + i, itemData.matrixPosition.y + j] = value;

                }
            }
        }
    }

    private void FindSlotPositionForItemCharacterPanel(List<ItemData> itemDataList) {
        InventoryPanel panel = GetPanel(InventoryPanel.Type.Character);

        foreach (ItemData itemData in itemDataList) {

            foreach (Transform slot in panel.slotParent) {

                if (slot.GetComponent<SlotData>().matrixPosition.y == itemData.matrixPosition.y) {
                    itemData.slotPosition = slot.GetComponent<RectTransform>().position;
                    break;
                }
            }
        }
    }

    public InventoryPanel GetPanel(InventoryPanel.Type panelType) {

        foreach (InventoryPanel panel in panelList) {

            if (panel.type == panelType) {
                return panel;
            }
        }
        return null;
    }

}