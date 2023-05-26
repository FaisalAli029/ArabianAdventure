using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] sectionPrefabs; // An array of prefabs for the map sections
    public float sectionLength = 25f; // The length of each map section
    public float generateDistance = 25f; // The distance ahead of the player at which new sections should be generated
    public GenerateObstecales obstacleSpawner; // A reference to the obstacle spawner component

    private Transform playerTransform; // Reference to the player's transform
    private List<GameObject> sections = new List<GameObject>(); // A list of the generated map sections

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        obstacleSpawner = GetComponentInChildren<GenerateObstecales>();
        GenerateSection();
    }

    private void Update()
    {
        if (playerTransform.position.z > generateDistance - (sections.Count * sectionLength))
        {
            GenerateSection();
        }
    }

    private void GenerateSection()
    {
        int index = Random.Range(0, sectionPrefabs.Length);
        Vector3 position = Vector3.forward * (sections.Count * sectionLength);
        GameObject sectionObject = Instantiate(sectionPrefabs[index], position, Quaternion.identity);
        sections.Add(sectionObject);

        // Find the spawn points in theinstantiated section prefab
        List<Transform> spawnPoints = new List<Transform>();
        Transform[] childTransforms = sectionObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms)
        {
            if (child != sectionObject.transform && child.tag == "SpawnPoint")
            {
                spawnPoints.Add(child);
            }
        }

        // Spawn obstacles randomly using the ObstacleSpawner script
        obstacleSpawner.SpawnObstaclesRandomly(spawnPoints.ToArray());
    }
}
