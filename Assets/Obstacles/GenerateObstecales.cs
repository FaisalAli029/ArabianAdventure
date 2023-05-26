using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstecales : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs to spawn
    public float spawnChance = 0.5f; // Chance of spawning an obstacle at each spawn point

    public void SpawnObstaclesRandomly(Transform[] spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Random.value < spawnChance)
            {
                int obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
                GameObject obstacle = Instantiate(obstaclePrefabs[obstacleIndex], spawnPoints[i].position, Quaternion.identity);
                obstacle.transform.parent = transform;
            }
        }
    }
}

