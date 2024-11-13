using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject[] Layouts;

    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private InGameHud _inGameHud;
    [SerializeField] private PauseMenu _pauseMenu;

    public bool IsPaused;

    // Start is called before the first frame update
    void Start() {
        // Call the function "ActivateMainMenu()"
        ActivateMainMenu();
    }

    // Update is called once per frame
    void Update() {
        // Call the function "SetPauseGameMenu()"
        SetPauseGameMenu();
    }

    // Function that call the function "SetMainMenu()"
    public void ActivateMainMenu() {
        _mainMenu.SetMainMenu();
    }

    // Function that call the function "SetInGameHud()"
    public void ActivateInGameHud() {
        _inGameHud.SetInGameHud();
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