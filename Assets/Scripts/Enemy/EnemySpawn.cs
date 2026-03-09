using UnityEngine;
using System.Collections;

public class SimpleEnemySpawner : MonoBehaviour
{
    [Header("Basic Settings")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f; 
    public int maxEnemies = 5;

    [Header("Spawn Zone")]
    public float minX, maxX;
    public float minY, maxY;

    [Header("Safe Zone")]
    public float safeDistance = 3f;

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

        Vector3 spawnPos;
        int attempts = 0;

        do
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            spawnPos = new Vector3(randomX, randomY, 0);
            attempts++;

        } while (Vector3.Distance(spawnPos, player.position) < safeDistance && attempts < 10);

        if (attempts < 10 || player == null)
        {
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(player.position, safeDistance);
        }
    }

}