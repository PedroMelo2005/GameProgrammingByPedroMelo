using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using TMPro;
using static SoundManager;

public class UIManager : MonoBehaviour {
    [SerializeField] private InGameHud _inGameHud;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private TMP_Text interactionText;

    public static UIManager Instance; // Global static instance

    private bool _isInGameHudOpen;
    private bool _isPauseMenuOpen;
    private bool _isInventoryMenuOpen;
    private bool _isCombatMenuOpen;
    private bool _isPuzzleMenuOpen;

    public bool IsInGameHudOpen { get { return _isInGameHudOpen; } }
    public bool IsPauseMenuOpen { get { return _isPauseMenuOpen; } }
    public bool IsInventoryMenuOpen { get { return _isInventoryMenuOpen; } }
    public bool IsCombatMenuOpen { get { return _isCombatMenuOpen; } }
    public bool IsPuzzleMenuOpen { get { return _isPuzzleMenuOpen; } }

    [SerializeField] private MenusLayout[] menusLayoutArray;

    [System.Serializable]
    public class MenusLayout {
        public GameObject menusObject;
        public Menus menus;
    }

    public enum Menus {
        InGameHud,
        PauseMenu,
        InventoryMenu,
        CombatMenu,
        PuzzleMenu
    }

    public void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        SetStartGame();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetMenuLayout(Menus menus, bool setState) {
        GameObject menuLayout = GetMenuLayout(menus);

        if (menuLayout != null) {
            menuLayout.SetActive(setState);
        }
        else {
            Debug.LogError("Error when SetMenuLayout");
        }

        /*
        foreach (MenusLayout menusLayout in menusLayoutArray) {
            GameObject otherMenuLayout = GetMenuLayout(menusLayout.menus);

            if (otherMenuLayout != null) {
                otherMenuLayout.SetActive(menusLayout.menus == menus);
            }
            else {
                Debug.LogError("Error when SetMenuLayout");
            }
        }
        */

    }

    private GameObject GetMenuLayout(Menus menus) {
        foreach (MenusLayout menusLayout in menusLayoutArray) {
            if (menusLayout.menus == menus) {
                return menusLayout.menusObject;
            }
        }
        Debug.LogError("Menu" + menus + " not found!");
        return null;
    }

    public void SetStartGame() {
        ShowInGameHud();
        HidePauseMenu();
        HideInventoryMenu();
    }

    public void EnableInteractionText(string text) {
        interactionText.text = text + " (E)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText() {
        interactionText.gameObject.SetActive(false);
    }

    // ShowInGameHud
    public void ShowInGameHud() {
        SetMenuLayout(Menus.InGameHud, true);
        _isInGameHudOpen = true;
    }

    // ShowInGameHud
    public void HideInGameHud() {
        SetMenuLayout(Menus.InGameHud, false);
        _isInGameHudOpen = false;
    }

    // ShowPauseMenu
    public void ShowPauseMenu() {
        SetMenuLayout(Menus.PauseMenu, true);
        GameManager.Instance.PauseGame(true);
        Player.Instance.ActivateCursor();
        _isPauseMenuOpen = true;
    }

    // HidePauseMenu
    public void HidePauseMenu() {
        SetMenuLayout(Menus.PauseMenu, false);
        GameManager.Instance.PauseGame(false);
        Player.Instance.DeactivateCursor();
        _isPauseMenuOpen = false;
    }

    // ShowInventoryMenu
    public void ShowInventoryMenu() {
        SetMenuLayout(Menus.InventoryMenu, true);
        Player.Instance.ActivateCursor();
        _isInventoryMenuOpen = true;
    }

    // HideInventoryMenu
    public void HideInventoryMenu() {
        SetMenuLayout(Menus.InventoryMenu, false);
        Player.Instance.DeactivateCursor();
        _isInventoryMenuOpen = false;
    }

}