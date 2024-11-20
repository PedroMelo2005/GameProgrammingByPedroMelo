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

    public static UIManager instance;
    private GameManager _gameManager;

    public bool IsInvnetoryMenuOpen = false;

    public void Awake() {
        instance = this;
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
        // Instantiate the gameManagerPrefab
        _gameManager = Instantiate(GameManagerPrefab);
        // Call function "OnStartGame()"
        _inGameHud.OnStartGame();
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
        if (Input.GetKeyDown(KeyCode.Tab) && IsInvnetoryMenuOpen == true) {
            _inventoryMenu.SetActive(false);
            IsInvnetoryMenuOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && IsInvnetoryMenuOpen == false) {
            _inventoryMenu.SetActive(true);
            IsInvnetoryMenuOpen = true;
        }
    }

}