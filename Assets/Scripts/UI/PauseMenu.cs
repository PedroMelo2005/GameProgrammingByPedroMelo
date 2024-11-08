using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Transform InventoryContentParent;
    [SerializeField] private InventoryItem InventoryItemPrefab;

    List<ItemData> _fakeInventoryForTesting = new();
    List<InventoryItem> _inventoryItemInstances = new();

    private void Awake() {
        /*
        _fakeInventoryForTesting.Add(new ItemData("Longsword", ItemData.Rarity.Common));
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable() {
        foreach (ItemData item in _fakeInventoryForTesting) {
            var inventoryItem = Instantiate(InventoryItemPrefab, InventoryContentParent);
            inventoryItem.Setup(item);
            _inventoryItemInstances.Add(inventoryItem);
        }
    }

    private void OnDisable() {
        foreach (InventoryItem item in _inventoryItemInstances) {
            Destroy(item.gameObject);
        }
        _inventoryItemInstances.Clear();
    }

    public void ButtonContinue() {

    }

    public void ButtonQuit() {

    }

}