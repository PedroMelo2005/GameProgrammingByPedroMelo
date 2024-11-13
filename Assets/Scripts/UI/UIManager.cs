using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameManager GameManagerPrefab;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private InGameHud _inGameHud;
    [SerializeField] private PauseMenu _pauseMenu;

    private GameManager _gameManager;

    public static bool IsMainMenuActive = false;
    public static bool IsPaused = false;

    // Start is called before the first frame update
    void Start() {
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (IsMainMenuActive == true) {
            // Call the function "ActivateMainMenu()"
            ActivateMainMenu();
        }
        else if (IsMainMenuActive == false) {
            // Call the function "SetStartGame()"
            SetStartGame();
        }
    }

    // Update is called once per frame
    void Update() {
        if (IsMainMenuActive == false) {
            // Call the function "SetPauseGameMenu()"
            SetPauseGameMenu();
        }
    }

    public void SetStartGame() {
        // Instantiate the gameManagerPrefab
        _gameManager = Instantiate(GameManagerPrefab);
        // Call function "OnStartGame()"
        _inGameHud.OnStartGame();
        // Lock the Cursor to the center of the game window and set "Cursor.visible" to false
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Function that call the function "SetMainMenu()"
    public void ActivateMainMenu() {
        _mainMenu.SetMainMenu();
    }

    // Call the functions "ContinueGame()" or "PauseGame()"
    public void SetPauseGameMenu() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused == true) {
                _pauseMenu.ContinueGame();
            }
            else if (IsPaused == false) {
                _pauseMenu.PauseGame();
            }
        }
    }

}