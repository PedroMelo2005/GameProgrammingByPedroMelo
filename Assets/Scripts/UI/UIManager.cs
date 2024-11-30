using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using TMPro;
using UnityEngine.UI;
using static SoundManager;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text _interactionText;
    [SerializeField] private InGameHud _inGameHud;
    [SerializeField] private PauseMenu _pauseMenu;
    public CanvasGroup canvasGroupInventory;
    public CanvasGroup canvasGroupMenu;
    [SerializeField] GameObject lootPanel;
    private GameManager gameManager;
    private PlayerManager playerManager;

    
    private static UIManager instance;
    public static UIManager Instance;

    private bool _isInGameHudOpen;
    private bool _isPauseMenuOpen;
    private bool _isInventoryMenuOpen;
    private bool _isCombatMenuOpen;
    private bool _isPuzzleMenuOpen;

    public bool IsInGameHudOpen => _isInGameHudOpen;
    public bool IsPauseMenuOpen => _isPauseMenuOpen;
    public bool IsInventoryMenuOpen => _isInventoryMenuOpen;
    public bool IsCombatMenuOpen => _isCombatMenuOpen;
    public bool IsPuzzleMenuOpen => _isPuzzleMenuOpen;

    public bool isLockedUI = false;

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

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void SetUpUiManager(GameManager gameManager, PlayerManager playerManager) {
        this.gameManager = gameManager;
        this.playerManager = playerManager;
        _inGameHud.SetUp(this);
        _pauseMenu.SetUp(this);
    }

    public void OnStartGame() {
        ShowInGameHud();
        HidePauseMenu();
        SetCanvasInventory(false);
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

    public void EnableInteractionText(string text) {
        _interactionText.text = text + " (E)";
        _interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText() {
        _interactionText.gameObject.SetActive(false);
    }

    public void SetCanvasInventory(bool value) {
        if (value) {
            canvasGroupInventory.alpha = 1;
            canvasGroupInventory.interactable = true;
            canvasGroupInventory.blocksRaycasts = true;
        }
        else {
            canvasGroupInventory.alpha = 0;
            canvasGroupInventory.interactable = false;
            canvasGroupInventory.blocksRaycasts = false;
        }
    }

    public void TurnCanvasInventory() {
        if (GetStatusOfCanvas(canvasGroupInventory)) {
            canvasGroupInventory.alpha = 0;
            canvasGroupInventory.interactable = false;
            canvasGroupInventory.blocksRaycasts = false;
        }
        else {
            canvasGroupInventory.alpha = 1;
            canvasGroupInventory.interactable = true;
            canvasGroupInventory.blocksRaycasts = true;
        }
    }

    public bool GetStatusOfCanvas(CanvasGroup canvasGroup) {
        if (canvasGroup.alpha == 1) {
            return true;
        }
        else {
            return false;
        }
    }

    public void SetActiveLootPanel(bool value) {
        lootPanel.SetActive(value);
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
        HideInGameHud();
        GameManager.Instance.PauseGame(true);
        Player.Instance.ActivateCursor();
        _isPauseMenuOpen = true;
    }

    // HidePauseMenu
    public void HidePauseMenu() {
        SetMenuLayout(Menus.PauseMenu, false);
        ShowInGameHud();
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