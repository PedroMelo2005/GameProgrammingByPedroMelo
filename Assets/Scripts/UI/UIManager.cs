using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject[] Layouts;

    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject inGameHudObject;
    [SerializeField] private GameObject pauseMenuObject;

    [SerializeField] private PauseMenu _pauseMenu;

    public bool IsPaused;

    // Start is called before the first frame update
    void Start() {
        OpenMainMenu();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            _pauseMenu.PauseGame();
        }
    }

    private void SetLayout(MenuLayout layout) {
        for (int i = 0; i < Layouts.Length; i++) {
            Layouts[i].SetActive((int) layout == i);
        }
    }

    public void OpenMainMenu() {
        mainMenuObject.SetActive(true);
        /*
        SetLayout(MenuLayout.Main);
        */
    }

    public void ActivateInGameHud() {
        inGameHudObject.SetActive(true);
        /*
        SetLayout(MenuLayout.InGame);
        */
    }

    public void ShowPauseGameMenu() {
        if (IsPaused == true) {
            pauseMenuObject.SetActive(false);
        }
        else if (IsPaused == false) {
            pauseMenuObject.SetActive(true);
        }
        /*
        SetLayout(MenuLayout.Pause);
        */
    }

}