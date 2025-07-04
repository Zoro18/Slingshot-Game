using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasHandler : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectionPanel;
    public GameObject optionsPanel;
    public GameObject shopPanel;

    [Header("Buttons")]
    public Button playButton;
    public Button optionsButton;
    public Button shopButton;
    public Button exitButton;
    public Button backButtonLevelSelection;
    public Button backButtonOptions;
    public Button backButtonShop;

    void Start()
    {
        // Ensure the main menu panel is active at the start
        ShowMainMenu();

        // Assign button click listeners
        playButton.onClick.AddListener(OpenLevelSelectionPanel);
        optionsButton.onClick.AddListener(OpenOptionsPanel);
        shopButton.onClick.AddListener(OpenShopPanel);
        exitButton.onClick.AddListener(ExitGame);
        backButtonLevelSelection.onClick.AddListener(ShowMainMenu);
        backButtonOptions.onClick.AddListener(ShowMainMenu);
        backButtonShop.onClick.AddListener(ShowMainMenu);
    }

    void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        optionsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    void OpenLevelSelectionPanel()
    {
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    void OpenOptionsPanel()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void OpenShopPanel()
    {
        mainMenuPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    void ExitGame()
    {
        // Note: Application.Quit() won't work in the editor; use UnityEditor.EditorApplication.isPlaying = false instead
        // when running inside the editor.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
