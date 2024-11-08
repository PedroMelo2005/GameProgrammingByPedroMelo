using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] private GameObject[] Layouts;

    // Start is called before the first frame update
    void Start()
    {
        OpenMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLayout(MenuLayout layout) {
        for (int i = 0; i < Layouts.Length; i++) {
            Layouts[i].SetActive((int) layout == i);
        }
    }

    public void OpenMainMenu() {
        SetLayout(MenuLayout.Main);
    }

    public void ActivateInGameHud() {
        SetLayout(MenuLayout.InGame);
    }

    public void ShowPauseGameMenu() {
        SetLayout(MenuLayout.Pause);
    }

}