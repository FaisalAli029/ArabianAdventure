using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateLevel : MonoBehaviour
{
    [Header("Map Sections")]
    [SerializeField] private GameObject[] sectionPrefabs;
    [SerializeField] private float sectionLength = 25f;

    [Header("Obstacles")]
    [SerializeField] private GenerateObstecales obstacleSpawner;

    [Header("Generation Settings")]
    [SerializeField] private int numSectionsToPreload = 50;
    [SerializeField] private int sectionsToGenerate = 50;

    private Transform playerTransform;
    private List<GameObject> sections;
    private int sectionsGenerated;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        obstacleSpawner = GetComponentInChildren<GenerateObstecales>();
        sections = new List<GameObject>();

        for (int i = 0; i < numSectionsToPreload; i++)
        {
            GenerateSection();
            sectionsGenerated++;
        }
    }

    private void Update()
    {
        int currentSection = Mathf.FloorToInt(playerTransform.position.z / sectionLength);

        if (currentSection > sectionsGenerated - numSectionsToPreload)
        {
            for (int i = 0; i < sectionsToGenerate; i++)
            {
                GenerateSection();
                sectionsGenerated++;
            }
        }
    }

    private void GenerateSection()
    {
        int index = Random.Range(0, sectionPrefabs.Length);
        Vector3 position = Vector3.forward * (sections.Count * sectionLength);
        GameObject sectionObject = Instantiate(sectionPrefabs[index], position, Quaternion.identity);
        sections.Add(sectionObject);

        // Find the spawn points in the instantiated section prefab
        List<Transform> spawnPoints = new List<Transform>();
        Transform[] childTransforms = sectionObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childTransforms)
        {
            if (child != sectionObject.transform && child.CompareTag("SpawnPoint"))
            {
                spawnPoints.Add(child);
            }
        }

        // Spawn obstacles randomly using the ObstacleSpawner script
        obstacleSpawner.SpawnObstaclesAndPowerUpsRandomly(spawnPoints.ToArray());
    }
}
