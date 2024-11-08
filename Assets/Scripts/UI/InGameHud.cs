using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHud : MonoBehaviour {
    [SerializeField] private Image HealthBar;
    [SerializeField] private Text Timer;

    private bool _gamePaused = true;
    private float _timer = 0f;

    // Start is called before the first frame update
    void Start() {
        Timer.text = "Timer Paused";
        Timer.color = Color.yellow;
    }

    // Update is called once per frame
    void Update() {
        if (_gamePaused) return;
        _timer += Time.deltaTime;
        Timer.text = $"{_timer,0:0.000}";
    }

    public void OnStartGame() {
        _gamePaused = false;
        HealthBar.fillAmount = 1;
    }

    public void OnPauseGame() {
        _gamePaused = true;
    }

    public void OnHealthChange(float currentHealth, float maxHealth) {
        HealthBar.fillAmount = currentHealth / maxHealth;
    }

}