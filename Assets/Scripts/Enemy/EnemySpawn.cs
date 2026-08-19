using UnityEngine;
using System.Collections;

public class SimpleEnemySpawner : MonoBehaviour
{
    [Header("Basic Settings")]
    public GameObject enemyPrefab;
    public float spawnRate = 2f; 
    public int maxEnemies = 5;
    public int countOfEnemies = 10;

    [Header("Spawn Zone")]
    public bool autoDetectedBound = true;
    public float minX, maxX;
    public float minY, maxY;

    private int spawnEnemies = 0;
    private SpriteRenderer spriteRenderer;

    void Start()
    {

        if (autoDetectedBound)
        {
            AutoDetectBounds();
        }

        // Запускаем спавн
        StartCoroutine(SpawnLoop());
    }

    void AutoDetectBounds()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Bounds bounds = spriteRenderer.bounds;

            minX = bounds.min.x;
            maxX = bounds.max.x;
            minY = bounds.min.y;
            maxY = bounds.max.y;
        }
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            // Проверяем количество врагов
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies && spawnEnemies <= countOfEnemies)
            {
                SpawnEnemy();
                spawnEnemies++;
            }
            else if(spawnEnemies >= countOfEnemies)
            {
                yield break;
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

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0);
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

}