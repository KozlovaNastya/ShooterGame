using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Настройки снаряда")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 2f;

    [Header("Эффекты")]
    [SerializeField] private GameObject hitEffect;

    private Rigidbody2D rb;
    private Vector2 movementDirection = Vector2.right; // По умолчанию вправо

    void Start()
    {
        Debug.Log($"Bullet Start called at {Time.time}");

        // Получаем Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet: No Rigidbody2D found! Adding one...");
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Автоуничтожение через время
        Destroy(gameObject, lifetime);

        Debug.Log($"Bullet initialized. Speed: {speed}, Direction: {movementDirection}");
    }

    // Вызывается из PlayerShooting
    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("Bullet: Zero direction received, using default (right)");
            direction = Vector2.right;
        }

        movementDirection = direction.normalized;
        Debug.Log($"Bullet direction set to: {movementDirection}");

        // Применяем движение сразу
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (rb == null)
        {
            Debug.LogError("Bullet: Cannot apply movement - no Rigidbody2D");
            return;
        }

        // Устанавливаем скорость
        rb.velocity = movementDirection * speed;
        Debug.Log($"Bullet velocity set to: {rb.velocity} (Speed: {rb.velocity.magnitude})");

        // Поворачиваем снаряд в направлении движения
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Debug.Log($"Bullet rotated to angle: {angle}°");
        }
    }

    void FixedUpdate()
    {
        // Дополнительная проверка: если скорость почему-то сбросилась
        if (rb != null && rb.velocity.magnitude < 0.1f && movementDirection != Vector2.zero)
        {
            Debug.LogWarning("Bullet velocity lost! Reapplying...");
            rb.velocity = movementDirection * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bullet hit: {other.name} (Tag: {other.tag})");

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        // Игнорируем столкновения с игроком и другими снарядами
        else if (other.CompareTag("Player") || other.CompareTag("Bullet"))
        {
            return;
        }
    }

    void OnDestroy()
    {
        Debug.Log($"Bullet destroyed at {Time.time}");
    }
}