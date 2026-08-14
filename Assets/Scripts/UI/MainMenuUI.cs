using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;


    void Start()
    {
        InitializeMenu();
        SetupButtons();
    }

    private void InitializeMenu()
    {
        if(mainMenuPanel != null)
            mainMenuPanel.SetActive(true);
    }

    private void SetupButtons()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        if (exitButton != null)
            exitButton.onClick.AddListener(ExitGame);
    }
    private void StartGame()
    {
        if(mainMenuPanel != null)
            mainMenuPanel.SetActive(false);
    }
    private void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }

    private void OnDestroy()
    {
        if(startButton != null)
            startButton.onClick.RemoveListener(StartGame);
        if (exitButton != null)
            exitButton.onClick.RemoveListener(ExitGame);
    }
}
