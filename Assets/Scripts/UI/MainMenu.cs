using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    /*
    [SerializeField] private GameManager GameManagerPrefab;
    */
    [SerializeField] private UIManager UiSystem;
    [SerializeField] private InGameHud GameHud;

    /*
    private GameManager _gameManager;
    */

    // Make this gameObject be active in the scene
    public void SetMainMenu() {
        gameObject.SetActive(true);
    }

    public void ButtonStartGame() {
        UIManager.IsMainMenuActive = false;
        // Set the SetActive of the gameObject to false
        gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");
        /*
        // Instantiate the gameManagerPrefab
        _gameManager = Instantiate(GameManagerPrefab);
        // Set the SetActive of the gameObject to false
        gameObject.SetActive(false);
        // Call function "ActivateInGameHud()"
        UiSystem.ActivateInGameHud();
        // Call function "OnStartGame()"
        GameHud.OnStartGame();
        */
    }

    public void QuitGame() {
        Application.Quit();
    }

}