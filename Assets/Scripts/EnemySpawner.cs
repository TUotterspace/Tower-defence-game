using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyFighter;               // The EnemyFighter prefab to spawn
    public Transform[] spawnPoints;               // Array to hold spawn points (Spawner 1, Spawner 2, Spawner 3)
    public float spawnInterval = 3f;              // How often the enemy should spawn (in seconds)

    private void Start()
    {
        // Start the spawning process
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Randomly select a spawn point from the array of spawn points
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Get the random spawn point
        Transform randomSpawnPoint = spawnPoints[randomIndex];

        // Instantiate the EnemyFighter prefab at the selected spawn point's position
        Instantiate(EnemyFighter, randomSpawnPoint.position, Quaternion.identity);
    }
}