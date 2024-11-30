using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class InventoryManager : Singleton<InventoryManager> {

    //Slot
    [SerializeField] GameObject slotPrefab; // For instantiate
    [SerializeField] Transform slotParentBackpack;
    [SerializeField] Transform slotParentLoot;

    public Transform itemParentCharacter;
    public Transform itemParentBackpack;
    public Transform itemParentLoot;

    //Item UI Prefabs
    [SerializeField] GameObject item1x1;
    [SerializeField] GameObject item1x2;
    [SerializeField] GameObject item1x3;
    [SerializeField] GameObject item1x4;
    [SerializeField] GameObject item1x5;
    [SerializeField] GameObject item2x2;

    //Dragging
    public bool isDraggingItem;
    public GameObject draggingItem;
    public float draggingItemSmoothFactor = 10f;
    public Transform draggingItemParent;

    //Color
    public Color agree;
    public Color disagree;

    //Panel
    public List<InventoryPanel> panelList = new List<InventoryPanel>();
    //
    public bool isLockedUI;
    private Transform lastSlot;

    void Start() {
        panelList = GetComponents<InventoryPanel>().ToList();
        isLockedUI = UIManager.Instance.isLockedUI;

        StartCoroutine(CreatePanel(GetPanel(InventoryPanel.Type.Backpack), null)); // Creating Backpack Panel
    }

    void Update() {

    }

    public IEnumerator CreatePanel(InventoryPanel panel, LootData lootData) {
        ClearPanel(panel.type);

        if (lootData != null) //for lootdata and loot panel
        {
            panel.size = lootData.size;
        }

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < panel.size.x; i++) // creating slots
        {
            for (int j = 0; j < panel.size.y; j++) {
                GameObject slot = Instantiate(slotPrefab, panel.slotParent);
                slot.transform.GetComponent<SlotData>().matrixPosition = new Vector2Int(i, j);
                slot.transform.GetComponent<SlotData>().panelType = panel.type;
                if (lootData != null) {
                    slot.transform.GetComponent<SlotData>().myLootContainer = lootData.gameObject;
                }
            }
        }
        yield return new WaitForEndOfFrame();

        panel.matrix = new bool[panel.size.x, panel.size.y];

        //Add items
        if (lootData != null) {
            FindSlotPositionForItem(panel.slotParent, lootData.itemList);
            panel.itemDataList = lootData.itemList; // list synced
        }
        else {
            FindSlotPositionForItem(panel.slotParent, panel.itemDataList); //!corrected
        }
        FillItem(panel, lootData);
    }

    public void ClearPanel(InventoryPanel.Type panelType) {
        InventoryPanel panel = GetPanel(panelType);

        if (panelType != InventoryPanel.Type.Character) {

            foreach (Transform slot in panel.slotParent) //removing slots
            {
                Destroy(slot.gameObject);
            }
        }

        foreach (Transform item in panel.itemParent) //removing items
        {
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

        if (lootData != null) { //for lootPanel

            foreach (ItemData itemData in lootData.itemList) {
                GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                newItem.transform.GetComponent<ItemUI>().Initialize();
                SetMatrixThanPanel(itemData, true);
            }
        }
        else { // for others Panel

            if (panel.type == InventoryPanel.Type.Backpack) {

                foreach (ItemData itemData in panel.itemDataList) { //!corrected
                    GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                    newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                    newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                    newItem.transform.GetComponent<ItemUI>().Initialize();
                    SetMatrixThanPanel(itemData, true);
                }
            }
            else if (panel.type == InventoryPanel.Type.Character) {
                FindSlotPositionForItemCharacterPanel(panel.itemDataList);

                foreach (ItemData itemData in panel.itemDataList) { //!corrected
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

        if (size == new Vector2Int(1, 1)) {
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

    public void SetMatrixThanPanel(ItemData itemData, bool value) {
        InventoryPanel panel = GetPanel(itemData.slotPanelType);
        Vector2Int itemSize = itemData.item.slotSize;

        //for character panel
        if (itemData.slotPanelType == InventoryPanel.Type.Character) {

            foreach (Transform slot in panel.slotParent) {

                if (slot.GetComponent<SlotData>().matrixPosition.y == itemData.matrixPosition.y) {
                    slot.GetComponent<SlotData>().isFull = value;
                    return;
                }
            }
        }

        //for other panels
        if (!itemData.isRotated) //if no rotated
        {
            for (int i = 0; i < itemSize.x; i++) {

                for (int j = 0; j < itemSize.y; j++) {
                    panel.matrix[itemData.matrixPosition.x + i, itemData.matrixPosition.y + j] = value;
                }
            }
        }
        else //if rotated
        {
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

    public void SetItemBlockRaycast(bool value) { //false: off, true: on

        foreach (InventoryPanel panel in panelList) { // close all raycast of items

            foreach (Transform item in panel.itemParent) {

                item.GetComponent<CanvasGroup>().blocksRaycasts = value;
            }
        }
    }

    public void CheckItemToPlace(GameObject item, Transform slot) { //for character panel

        if (!slot.GetComponent<SlotData>().isFull) { //false: avaible
            slot.GetComponent<SlotData>().isFull = true;
            item.GetComponent<ItemDataMB>().itemData.matrixPosition.y = slot.GetComponent<SlotData>().matrixPosition.y;
            InventoryPanel previousPanel = GetPanel(item.GetComponent<ItemDataMB>().itemData.slotPanelType);
            SetItemParent(item, GetPanel(InventoryPanel.Type.Character).itemParent);

            //check the rotate
            if (slot.GetComponent<SlotData>().slotType == Item.SlotType.Weapon) {

                if (item.GetComponent<ItemDataMB>().itemData.isRotated) { //if rotated, set to no rotated
                    item.transform.GetChild(0).gameObject.SetActive(true);
                    item.transform.GetChild(1).gameObject.SetActive(false);
                    item.transform.GetComponent<ItemDataMB>().itemData.isRotated = false;
                }
                float itemWidth = item.GetComponent<RectTransform>().sizeDelta.x;
                float slotWidth = slot.GetComponent<RectTransform>().sizeDelta.x;
                float deltaWidth = (slotWidth - itemWidth) / 2f;
                item.GetComponent<RectTransform>().position = slot.transform.GetComponent<RectTransform>().position + new Vector3(deltaWidth, 0);
            }
            else {
                item.GetComponent<RectTransform>().position = slot.transform.GetComponent<RectTransform>().position;
            }
            //
            UpdateItemDataListThanPanel(item, GetPanel(InventoryPanel.Type.Character), previousPanel);
            item.GetComponent<ItemDataMB>().itemData.slotPanelType = InventoryPanel.Type.Character;
        }
        else {
            item.GetComponent<ItemUI>().RestartPosition();
        }
    }
    public void CheckItemMatrixToPlace(GameObject item, Transform slot) {
        Vector2Int itemSize = item.GetComponent<ItemDataMB>().itemData.item.slotSize;
        Vector2Int slotMatrixPosition = slot.GetComponent<SlotData>().matrixPosition;
        InventoryPanel panel = GetPanel(slot.transform.GetComponent<SlotData>().panelType);
        InventoryPanel previousPanel = GetPanel(item.GetComponent<ItemDataMB>().itemData.slotPanelType);

        if (CheckMatrix(item, slot)) { //false: full, true: available

            if (!item.GetComponent<ItemDataMB>().itemData.isRotated) { // if no rotated

                for (int i = 0; i < itemSize.x; i++) {

                    for (int j = 0; j < itemSize.y; j++) {
                        panel.matrix[slotMatrixPosition.x + i, slotMatrixPosition.y + j] = true;
                    }
                }
            }
            else // if rotated
            {
                for (int i = 0; i < itemSize.y; i++) {

                    for (int j = 0; j < itemSize.x; j++) {
                        panel.matrix[slotMatrixPosition.x + i, slotMatrixPosition.y + j] = true;
                    }
                }
            }
            item.GetComponent<RectTransform>().position = slot.transform.GetComponent<RectTransform>().position;
            SetItemParent(item, panel.itemParent);
            item.GetComponent<ItemDataMB>().itemData.matrixPosition = slotMatrixPosition;
            UpdateItemDataListThanPanel(item, panel, previousPanel);
            item.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
        }
        else {
            item.GetComponent<ItemUI>().RestartPosition();
        }
    }

    public bool CheckMatrix(GameObject item, Transform slot) {
        Vector2Int itemSize = item.GetComponent<ItemDataMB>().itemData.item.slotSize;
        Vector2Int slotMatrixPosition = slot.GetComponent<SlotData>().matrixPosition;
        InventoryPanel panel = GetPanel(slot.transform.GetComponent<SlotData>().panelType);
        int row = panel.matrix.GetLength(0);
        int column = panel.matrix.GetLength(1);

        if (!item.GetComponent<ItemDataMB>().itemData.isRotated) {

            if (slotMatrixPosition.y + itemSize.y <= column && slotMatrixPosition.x + itemSize.x <= row) { // Is there enough area and no rotate

                for (int i = 0; i < itemSize.x; i++) {

                    for (int j = 0; j < itemSize.y; j++) {

                        if (panel.matrix[slotMatrixPosition.x + i, slotMatrixPosition.y + j] == true) {
                            return false;
                        }
                    }
                }
                return true; // Available
            }
            else {
                return false;
            }
        }
        else {

            if (slotMatrixPosition.y + itemSize.x <= column && slotMatrixPosition.x + itemSize.y <= row) { //is there enough area and no rotate

                for (int i = 0; i < itemSize.y; i++) {

                    for (int j = 0; j < itemSize.x; j++) {

                        if (panel.matrix[slotMatrixPosition.x + i, slotMatrixPosition.y + j] == true) {
                            return false;
                        }
                    }
                }
                return true; // Available
            }
            else {
                return false;
            }
        }
    }

    public void SetItemParent(GameObject item, Transform parent) {
        item.transform.SetParent(parent);
    }

    public void UpdateItemDataListThanPanel(GameObject item, InventoryPanel newPanel, InventoryPanel previousPanel) {

        if (previousPanel.type != newPanel.type) {
            previousPanel.itemDataList.Remove(item.GetComponent<ItemDataMB>().itemData);
            newPanel.itemDataList.Add(item.GetComponent<ItemDataMB>().itemData);
        }
    }

    public void ColorHelper(GameObject item, Transform slot) {
        lastSlot = slot;
        InventoryPanel panel = GetPanel(slot.transform.GetComponent<SlotData>().panelType);
        ItemData itemData = item.GetComponent<ItemDataMB>().itemData;

        if (panel.type == InventoryPanel.Type.Character) { //for character panel

            if (!slot.transform.GetComponent<SlotData>().isFull && slot.transform.GetComponent<SlotData>().slotType == item.GetComponent<ItemDataMB>().itemData.item.slotType) {

                if (!itemData.isRotated) {
                    item.transform.GetChild(0).transform.GetComponent<Image>().color = agree;
                }
                else //if rotated
                {
                    item.transform.GetChild(1).transform.GetComponent<Image>().color = agree;
                }
            }
            else {

                if (!itemData.isRotated) {
                    item.transform.GetChild(0).transform.GetComponent<Image>().color = disagree;
                }
                else //if rotated
                {
                    item.transform.GetChild(1).transform.GetComponent<Image>().color = disagree;
                }
            }
            return;
        }

        //for other panels
        if (CheckMatrix(item, slot)) { //true: available, false: full

            if (!itemData.isRotated) {
                item.transform.GetChild(0).transform.GetComponent<Image>().color = agree;
            }
            else // If rotated
            {
                item.transform.GetChild(1).transform.GetComponent<Image>().color = agree;
            }
        }
        else {

            if (!itemData.isRotated) {
                item.transform.GetChild(0).transform.GetComponent<Image>().color = disagree;
            }
            else // If rotated
            {
                item.transform.GetChild(1).transform.GetComponent<Image>().color = disagree;
            }
        }
    }

    public void RefreshItem() {
        ColorHelper(draggingItem, lastSlot);
    }

    public void RemoveItem(ItemData itemData) {
        InventoryPanel panel = GetPanel(itemData.slotPanelType);

        foreach (Transform item in panel.itemParent) {
            if (item.GetComponent<ItemDataMB>().itemData == itemData) {
                SetMatrixThanPanel(item.GetComponent<ItemDataMB>().itemData, false);
                Destroy(item.gameObject);
                break;
            }
        }
        panel.itemDataList.Remove(itemData);
    }

    public void TakeItem(GameObject takedItem) {
        ItemData itemData = takedItem.GetComponent<ItemDataMB>().itemData;
        InventoryPanel panel = GetPanel(InventoryPanel.Type.Backpack);
        Vector2Int itemSize = itemData.item.slotSize;
        int column = panel.matrix.GetLength(1);
        int row = panel.matrix.GetLength(0);

        for (int i = 0; i < row; i++) {
            for (int j = 0; j < column; j++) {
                if (i + itemSize.x <= row && j + itemSize.y <= column) //is there enough area. NO ROTATE
                {
                    bool status = false; // false: empyt, true: full
                    for (int x = 0; x < itemSize.x; x++) {
                        for (int y = 0; y < itemSize.y; y++) {
                            if (panel.matrix[i + x, j + y] == true) {
                                status = true;
                            }
                        }
                    }

                    if (status == false) // Available
                    {
                        itemData.isRotated = false;
                        GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                        newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                        newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                        newItem.transform.GetComponent<ItemDataMB>().itemData.matrixPosition = new Vector2Int(i, j);
                        newItem.transform.GetComponent<ItemDataMB>().itemData.slotPosition = GetSlotPosition(itemData, InventoryPanel.Type.Backpack);
                        newItem.transform.GetComponent<ItemUI>().Initialize();
                        SetMatrixThanPanel(itemData, true);
                        panel.itemDataList.Add(itemData);
                        Destroy(takedItem);
                        return;
                    }
                }
                if (i + itemSize.y <= row && j + itemSize.x <= column) //is there enough area. ROTATED
                {
                    bool status = false; // false: empty, true: full
                    for (int x = 0; x < itemSize.y; x++) {
                        for (int y = 0; y < itemSize.x; y++) {
                            if (panel.matrix[i + x, j + y] == true) {
                                status = true;
                            }
                        }
                    }

                    if (status == false) // Available
                    {
                        itemData.isRotated = true;
                        GameObject newItem = Instantiate(GetMyPrefab(itemData.item.slotSize), panel.itemParent);
                        newItem.transform.GetComponent<ItemDataMB>().itemData = itemData;
                        newItem.transform.GetComponent<ItemDataMB>().itemData.slotPanelType = panel.type;
                        newItem.transform.GetComponent<ItemDataMB>().itemData.matrixPosition = new Vector2Int(i, j);
                        newItem.transform.GetComponent<ItemDataMB>().itemData.slotPosition = GetSlotPosition(itemData, InventoryPanel.Type.Backpack);
                        newItem.transform.GetComponent<ItemUI>().Initialize();
                        SetMatrixThanPanel(itemData, true);
                        panel.itemDataList.Add(itemData);
                        Destroy(takedItem);
                        return;
                    }
                }
            }
        }
    }

    public Vector3 GetSlotPosition(ItemData itemData, InventoryPanel.Type panelType) {
        InventoryPanel panel = GetPanel(panelType);

        foreach (Transform slot in panel.slotParent) {
            if (slot.GetComponent<SlotData>().matrixPosition == itemData.matrixPosition) {
                return slot.transform.GetComponent<RectTransform>().position;
            }
        }
        return Vector3.zero;
    }
}