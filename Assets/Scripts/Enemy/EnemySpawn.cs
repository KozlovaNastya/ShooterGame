using UnityEngine;
using System.Collections;

public class SimpleEnemySpawner : MonoBehaviour
{
    [Header("Basic Settings")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f;    // Раз в 2 секунды
    public int maxEnemies = 5;

    [Header("Spawn Zone")]
    public float spawnRadius = 8f;  // Радиус от центра

    private Transform player;

    void Start()
    {
        // Находим игрока
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Запускаем спавн
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Проверяем количество врагов
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Assign enemy prefab in inspector!");
            return;
        }

        // Случайная позиция по кругу
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, randomCircle.y, 0);

        // Создаем врага
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        Debug.Log($"Spawned enemy at {spawnPos}");
    }

    // Для визуализации в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}