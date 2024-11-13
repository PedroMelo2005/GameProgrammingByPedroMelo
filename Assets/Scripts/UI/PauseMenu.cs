using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private Transform InventoryContentParent;
    [SerializeField] private InventoryItem InventoryItemPrefab;

    [SerializeField] private UIManager UiSystem;

    List<ItemData> _fakeInventoryForTesting = new();
    List<InventoryItem> _inventoryItemInstances = new();

    private void Awake() {
        /*
        _fakeInventoryForTesting.Add(new ItemData("Longsword", ItemData.Rarity.Common));
        */
    }

    // Start is called before the first frame update
    // Make this gameObject don't be active in the scene
    void Start() {
        gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update() {

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

    // Set PauseGame actions
    public void PauseGame() {
        // Make this gameObject be active in the scene
        gameObject.SetActive(true);
        // Freeze the time of the game
        Time.timeScale = 0f;
        // Assign the variable "IsPaused" to true
        UiSystem.IsPaused = true;
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Set ContinueGame actions
    public void ContinueGame() {
        // Make this gameObject don't be active in the scene
        gameObject.SetActive(false);
        // Unfreeze the time of the game
        Time.timeScale = 1f;
        // Assign the variable "IsPaused" to false
        UiSystem.IsPaused = false;
        // Lock the Cursor to the center of the game window and set "Cursor.visible" to false
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame() {

    }

}