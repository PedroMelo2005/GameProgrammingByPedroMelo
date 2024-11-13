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
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            ContinueGame();
        }
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

    public void PauseGame() {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        UiSystem.IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueGame() {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        UiSystem.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame() {

    }

}