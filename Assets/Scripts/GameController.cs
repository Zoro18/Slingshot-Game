using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // UI Buttons
    public Button forwardButton;
    public Button restartButton;
    public Button restartButton2;
    public Button continueButton;
    public Button Homebutton;
    public Button pauseButton;
    public Button quitButton;

    // Pause Panel
    public GameObject pausePanel;

    void Start()
    {
        // Assign button click events
        forwardButton.onClick.AddListener(LoadNextScene);
        restartButton.onClick.AddListener(RestartScene);
        restartButton2.onClick.AddListener(RestartScene);
        continueButton.onClick.AddListener(ContinueGame);
        pauseButton.onClick.AddListener(PauseGame);
        quitButton.onClick.AddListener(QuitToMainMenu);
        Homebutton.onClick.AddListener(QuitToMainMenu);

        // Ensure the pause panel is disabled at the start
        pausePanel.SetActive(false);
    }

    // Function to load the next scene
    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Time.timeScale = 1f;
    }

    // Function to restart the current scene
    void RestartScene()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("run");
        Time.timeScale = 1f;
    }

    // Function to continue the game by disabling the pause panel
    void ContinueGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game time
    }

    // Function to pause the game by enabling the pause panel
    void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game time
    }

    // Function to quit to the main menu scene
    void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
