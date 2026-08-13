using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public GameObject[] hearts;
    [Header("References")]
    [SerializeField] private DeathScreenUI deathScreenUI;
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;
    [Header("Events")]
    public UnityEvent OnPlayerDeath;

    private void Start()
    {
        if (deathScreenUI == null)
        {
            deathScreenUI = FindObjectOfType<DeathScreenUI>();
        }
    }
    public void TakeDamage()
    {
        if (health <= 0) return;

        health--;
        if (health < hearts.Length)
        {
            hearts[health].SetActive(false);
        }

        Debug.Log("Жизней осталось: " + health);

        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player died!");
        OnPlayerDeath?.Invoke();

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (playerCollider != null)
            playerCollider.enabled = false;

        if (deathScreenUI != null)
        {
            deathScreenUI.ShowDeathScreen();
        }
        else
        {
            Debug.LogError("DeathScreenUI not found!");
        }
    }
}
