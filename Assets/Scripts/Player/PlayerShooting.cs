using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("������ �������")]
    [SerializeField] private GameObject bulletPrefab;

    [Header("��������� ��������")]
    [SerializeField] private float fireRate = 0.3f;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("����������")]
    [SerializeField] private bool allowDiagonalShots = true;

    private float nextFireTime = 0f;
    private Vector2 lastShootDirection = Vector2.right;

    void Update()
    {
        HandleShootingInput();
    }

    void HandleShootingInput()
    {
        Vector2 shootDirection = GetArrowInputDirection();

        if (shootDirection != Vector2.zero)
        {
            lastShootDirection = shootDirection;

            if (Time.time >= nextFireTime)
            {
                Shoot(lastShootDirection);
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    Vector2 GetArrowInputDirection()
    {
        Vector2 direction = Vector2.zero;

        // ������� � �������� ������
        if (Input.GetKey(KeyCode.RightArrow)) direction.x = 1;
        else if (Input.GetKey(KeyCode.LeftArrow)) direction.x = -1;

        if (Input.GetKey(KeyCode.UpArrow)) direction.y = 1;
        else if (Input.GetKey(KeyCode.DownArrow)) direction.y = -1;

        // ��������� ������ ���� ���������
        if (!allowDiagonalShots)
        {
            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0)
            {
                // �������� ������������� �����������
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > Mathf.Abs(Input.GetAxisRaw("Vertical")))
                    direction.y = 0;
                else
                    direction.x = 0;
            }
        }

        return direction.normalized;
    }

    void Shoot(Vector2 direction)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("PlayerShooting: No bullet prefab assigned!");
            return;
        }

        // ������� ������
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Debug.Log($"Player: Created bullet at {transform.position}, direction: {direction}");

        // ����������� ������
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(direction);
        }
        else
        {
            Debug.LogError("PlayerShooting: Bullet prefab has no Bullet script!");

            // ���������� �������: ����������� �������
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    // ��� ������������ � ���������
    [ContextMenu("����: ������� ������")]
    void TestShootRight()
    {
        Shoot(Vector2.right);
    }
}