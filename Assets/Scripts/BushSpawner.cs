using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    [Header("Bush Prefabs")]
    public GameObject healthyBushPrefab;
    public GameObject damagedBush1Prefab;
    public GameObject damagedBush2Prefab;

    [Header("Spawner Settings")]
    public Transform bushParent;          // Parent container
    public float spacing = 5f;            // Distance between bushes
    public Vector2 areaSize = new Vector2(100, 100);

    [Header("Damage Settings")]
    [Range(0, 100)] public float damagedPercentage = 30f;   // % of bushes that are damaged
    [Range(0, 100)] public float damaged1Probability = 50f; // % of damaged bushes that are type1

    public int seed = 0;                  // Random seed

    void Start()
    {
        if (seed != 0) Random.InitState(seed);
        SpawnBushes();
    }

    void SpawnBushes()
    {
        if (healthyBushPrefab == null || damagedBush1Prefab == null || damagedBush2Prefab == null || bushParent == null)
        {
            Debug.LogError("Please assign all bush prefabs and bushParent!");
            return;
        }

        Vector3 startPos = bushParent.position;

        for (float x = -areaSize.x / 2; x < areaSize.x / 2; x += spacing)
        {
            for (float z = -areaSize.y / 2; z < areaSize.y / 2; z += spacing)
            {
                float offsetX = Random.Range(-spacing / 2f, spacing / 2f);
                float offsetZ = Random.Range(-spacing / 2f, spacing / 2f);

                Vector3 spawnPos = startPos + new Vector3(x + offsetX, 0f, z + offsetZ);
                Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360f), 0);

                // Decide if this bush is damaged
                float roll = Random.Range(0f, 100f);
                GameObject prefabToSpawn;

                if (roll < damagedPercentage)
                {
                    // This is a damaged bush -> choose between type1 and type2
                    float damagedRoll = Random.Range(0f, 100f);
                    if (damagedRoll < damaged1Probability)
                        prefabToSpawn = damagedBush1Prefab;
                    else
                        prefabToSpawn = damagedBush2Prefab;
                }
                else
                {
                    prefabToSpawn = healthyBushPrefab;
                }

                Instantiate(prefabToSpawn, spawnPos, rot, bushParent);
            }
        }
    }
}
