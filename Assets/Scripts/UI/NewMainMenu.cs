using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMainMenu : MonoBehaviour {
    public const string GameSceneName = "SampleScene";

    // Start is called before the first frame update
    void Start() {
        // Lock the Cursor to the game window and set "Cursor.visible" to true
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ButtonStartGame() {
        SceneManager.LoadScene(GameSceneName);
    }

    public void ButtonQuitGame() {
        Application.Quit();
    }

}