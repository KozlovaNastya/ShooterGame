using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("��������� �������")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 2f;

    [Header("�������")]
    [SerializeField] private GameObject hitEffect;

    private Rigidbody2D rb;
    private Vector2 movementDirection = Vector2.right; // �� ��������� ������

    void Start()
    {
        Debug.Log($"Bullet Start called at {Time.time}");

        // �������� Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet: No Rigidbody2D found! Adding one...");
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // ��������������� ����� �����
        Destroy(gameObject, lifetime);

        Debug.Log($"Bullet initialized. Speed: {speed}, Direction: {movementDirection}");
    }

    // ���������� �� PlayerShooting
    public void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("Bullet: Zero direction received, using default (right)");
            direction = Vector2.right;
        }

        movementDirection = direction.normalized;
        Debug.Log($"Bullet direction set to: {movementDirection}");

        // ��������� �������� �����
        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (rb == null)
        {
            Debug.LogError("Bullet: Cannot apply movement - no Rigidbody2D");
            return;
        }

        // ������������� ��������
        rb.linearVelocity = movementDirection * speed;
        Debug.Log($"Bullet velocity set to: {rb.linearVelocity} (Speed: {rb.linearVelocity.magnitude})");

        // ������������ ������ � ����������� ��������
        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            Debug.Log($"Bullet rotated to angle: {angle}�");
        }
    }

    void FixedUpdate()
    {
        // �������������� ��������: ���� �������� ������-�� ����������
        if (rb != null && rb.linearVelocity.magnitude < 0.1f && movementDirection != Vector2.zero)
        {
            Debug.LogWarning("Bullet velocity lost! Reapplying...");
            rb.linearVelocity = movementDirection * speed;
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
        // ���������� ������������ � ������� � ������� ���������
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