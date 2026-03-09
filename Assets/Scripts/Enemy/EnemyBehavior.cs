using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float stoppingDistance = 1f;

    [Header("Player Reference")]
    [SerializeField] private Transform playerTarget;

    [Header("Components")]
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            playerTarget = playerObj.transform;
            return;
        }
    }

    void Update()
    {
        if (playerTarget == null) return;

        Vector2 direction = (playerTarget.position - transform.position).normalized;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer > stoppingDistance)
        {
            MoveTowardsPlayer(direction);
        }
        else
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    void MoveTowardsPlayer(Vector2 direction)
    {
        if (rb != null)
        {
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage();

            Destroy(gameObject);
        }
    }
}
