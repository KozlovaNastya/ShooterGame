using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private Button retryButton;

    [Header("Optional")]
    [SerializeField] private TextMeshProUGUI deathMessageText;

    private bool isGameOver = false;

    void Start()
    {
        Debug.Log("DeathScreenUI Start called");

        if (deathPanel != null)
        {
            deathPanel.SetActive(false);
            Debug.Log("DeathPanel hidden at start");
        }
        else
        {
            Debug.LogError("DeathPanel is NULL! Assign it in inspector.");
        }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryClicked);
            Debug.Log("Retry button listener added");
        }
    }

    public void ShowDeathScreen()
    {
        if (isGameOver) return;
        isGameOver = true;

        Debug.Log("=== SHOW DEATH SCREEN CALLED ===");
        Debug.Log($"deathPanel is {(deathPanel == null ? "NULL" : "assigned")}");

        if (deathPanel != null)
        {
            deathPanel.SetActive(true);
            Debug.Log($"DeathPanel activated. Active state: {deathPanel.activeSelf}");
            Debug.Log($"DeathPanel position: {deathPanel.transform.position}");
            Debug.Log($"DeathPanel scale: {deathPanel.transform.localScale}");
        }
        else
        {
            Debug.LogError("DeathPanel is NULL! Cannot show death screen.");
        }

        Time.timeScale = 0f;
        Debug.Log("Game paused (Time.timeScale = 0)");
    }

    public void HideDeathScreen()
    {
        if (deathPanel != null)
            deathPanel.SetActive(false);

        isGameOver = false;
        Time.timeScale = 1f;
        Debug.Log("DeathScreen hidden, game resumed");
    }

    void OnRetryClicked()
    {
        Debug.Log("Retry button clicked - restarting level");

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}