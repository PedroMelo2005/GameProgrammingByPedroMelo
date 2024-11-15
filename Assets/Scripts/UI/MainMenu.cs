using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

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
        SceneManager.LoadScene(ConstVariables.GameSceneName);
    }

    public void ButtonQuitGame() {
        Application.Quit();
    }

}