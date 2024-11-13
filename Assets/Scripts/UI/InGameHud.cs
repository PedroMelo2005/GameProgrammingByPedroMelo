using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour {
    [SerializeField] private UIManager UiSystem;
    [SerializeField] private Image HealthBar;
    [SerializeField] private Text Timer;

    private float _timer = 0f;

    // Start is called before the first frame update
    void Start() {
        Timer.text = "Timer Paused";
        Timer.color = Color.yellow;
    }

    // Update is called once per frame
    void Update() {
        _timer += Time.deltaTime;
        Timer.text = $"{_timer,0:0.000}";
    }

    // Set some things when this function is called
    public void OnStartGame() {
        UIManager.IsPaused = false;
        HealthBar.fillAmount = 1;
    }

    // Make this gameObject be active in the scene
    public void SetInGameHud() {
        gameObject.SetActive(true);
    }

    public void OnHealthChange(float currentHealth, float maxHealth) {
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

}