using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Setting")]
    [SerializeField] private int maxHealth = 30;
    [SerializeField] private int currentHealth;

    [Header("Visual Effects")]
    [SerializeField] private Color hitColor = Color.white;
    [SerializeField] private float hitFlashTime = 0.1f;
    [SerializeField] private GameObject deathEffect;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FlashOnHit();
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void FlashOnHit()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = hitColor;
            Invoke(nameof(ResetColor), hitFlashTime);
        }
    }

    void ResetColor()
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
