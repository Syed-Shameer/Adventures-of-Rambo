using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform background; // The background area where enemies spawn
    public float minSpawnInterval = 2f; // Minimum time between spawns
    public float maxSpawnInterval = 5f; // Maximum time between spawns
    public int maxEnemies = 5; // Maximum number of enemies allowed in the scene

    private float spawnTimer;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        ResetSpawnTimer();
    }

    private void Update()
    {
        // Update the spawn timer
        spawnTimer -= Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (spawnTimer <= 0f && spawnedEnemies.Count < maxEnemies)
        {
            SpawnEnemy();
            ResetSpawnTimer();
        }

        // Clean up the list by removing null entries (destroyed enemies)
        spawnedEnemies.RemoveAll(enemy => enemy == null);
    }

    void SpawnEnemy()
    {
        // Get background bounds
        Vector3 backgroundSize = background.localScale;
        Vector3 backgroundPosition = background.position;

        // Generate a random position within background bounds
        float randomX = Random.Range(backgroundPosition.x - backgroundSize.x / 2, backgroundPosition.x + backgroundSize.x / 2);
        float randomY = Random.Range(backgroundPosition.y - backgroundSize.y / 2, backgroundPosition.y + backgroundSize.y / 2);

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // Instantiate enemy and add to the list
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        spawnedEnemies.Add(newEnemy);

        // Optional: Set up a callback to remove the enemy from the list when it's destroyed
        newEnemy.GetComponent<EnemyHealth>().OnDeath += () => spawnedEnemies.Remove(newEnemy);
    }

    void ResetSpawnTimer()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
