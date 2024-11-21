using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using TMPro;

public class InGameHud : MonoBehaviour {
    [SerializeField] private Image HealthBar;
    [SerializeField] private TMP_Text Timer;

    private float _timer = 0f;

    // Start is called before the first frame update
    void Start() {
        Timer.text = "Timer Paused";
    }

    // Update is called once per frame
    void Update() {
        _timer += Time.deltaTime;
        Timer.text = $"Time played: {_timer, 0:0.00}";
        OnHealthChange(); // Just testing
    }

    // Set some things when this function is called
    public void OnStartGame() {
        HealthBar.fillAmount = Player._maxPlayerLife;
        ActivateInGameHud();
    }

    // Make this gameObject be active in the scene
    public void ActivateInGameHud() {
        gameObject.SetActive(true);
    }

    public void DeactivateInGameHud() {
        gameObject.SetActive(false);
    }

    public void OnHealthChange() { // Maybe just testing ?
        HealthBar.fillAmount = Player.Instance.PlayerLife / Player._maxPlayerLife;
    }

}