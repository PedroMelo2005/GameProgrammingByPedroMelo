using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameManager GameManagerPrefab;
    [SerializeField] private InGameHud _inGameHud;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private GameObject _inventoryMenu;
    [SerializeField] private TMP_Text interactionText;

    public static UIManager Instance;
    private GameManager _gameManager;

    public bool IsInventoryMenuOpen = false;

    public void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        SetStartGame();
    }

    // Update is called once per frame
    void Update() {
        // Call the function "CheckPauseGameMenu()"
        CheckPauseMenu();
        // Call the function "CheckInventoryMenu()"
        CheckInventoryMenu();
    }

    public void SetStartGame() {
        // Call functions "OnStartGame()"
        _inGameHud.OnStartGame();
        _pauseMenu.OnStartGame();
    }

    // Check if the player pressed the keyboard key "Escape(Esc)" and call the respective function
    public void CheckPauseMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.IsGamePaused == true) {
                _pauseMenu.ButtonContinueGame();
            }
            else if (GameManager.IsGamePaused == false) {
                _pauseMenu.PauseGame();
            }
        }
    }

    public void EnableInteractionText(string text) {
        interactionText.text = text + " (E)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText() {
        interactionText.gameObject.SetActive(false);
    }

    public void CheckInventoryMenu() {
        if (Input.GetKeyDown(KeyCode.Tab) && IsInventoryMenuOpen == true) {
            _inventoryMenu.SetActive(false);
            IsInventoryMenuOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && IsInventoryMenuOpen == false) {
            _inventoryMenu.SetActive(true);
            IsInventoryMenuOpen = true;
        }
    }

}