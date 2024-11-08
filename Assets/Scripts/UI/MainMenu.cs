using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] GameManager gameManagerPrefab;
    [SerializeField] private UIManager UiSystem;
    [SerializeField] private InGameHud GameHud;

    private GameManager gameManager;

    public void ButtonStartGame() {
        // Instantiate the gameManagerPrefab
        gameManager = Instantiate(gameManagerPrefab);
        // Set the SetActive of the gameObject to false
        gameObject.SetActive(false);
        // Call function "ActivateInGameHud()"
        UiSystem.ActivateInGameHud();
        // Call function "OnStartGame()"
        GameHud.OnStartGame();
    }

}