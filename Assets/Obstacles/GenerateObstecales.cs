using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstecales : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs to spawn
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs to spawn
    public float obstacleSpawnChance = 0.5f; // Chance of spawning an obstacle at each spawn point
    public float powerUpSpawnChance = 0.1f; // Chance of spawning a power-up at each spawn point

    public void SpawnObstaclesAndPowerUpsRandomly(Transform[] spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.value < obstacleSpawnChance)
            {
                int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPoints[i].position, Quaternion.identity);
                obstacle.transform.parent = transform;
            }
            else if (Random.value < powerUpSpawnChance)
            {
                int powerUpIndex = Random.Range(0, powerUpPrefabs.Length);
                GameObject powerUp = Instantiate(powerUpPrefabs[powerUpIndex], spawnPoints[i].position, Quaternion.identity);
                powerUp.transform.parent = transform;
            }
        }
    }
}

