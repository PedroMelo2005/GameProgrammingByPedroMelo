using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    private UIManager uiManager;

    // Start is called before the first frame update
    void Start() {

    }
    
    // Update is called once per frame
    void Update() {

    }

    public void SetUp(UIManager uiManager) {
        this.uiManager = uiManager;
    }

    // Set ContinueGame actions
    public void ButtonContinueGame() {
        UIManager.Instance.HidePauseMenu();
    }

    public void ButtonBackToMainMenu() {
        // Change the Scene of the game
        SceneManager.LoadScene(ConstVariables.MainMenuSceneName);
        GameManager.Instance.PauseGame(false);
        Player.Instance.ActivateCursor();
    }

}