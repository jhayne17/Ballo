using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject platformPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public int initialPlatforms = 10;
    public float verticalSpacing = 2f;
    public float spawnDistance = 20f;
    public float despawnDistance = 10f;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private float lastY = 0f;

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (player.position.y - lastY < spawnDistance)
        {
            SpawnPlatform();
        }

        if (spawnedPlatforms.Count > 0)
        {
            GameObject firstPlatform = spawnedPlatforms[0];

            if (firstPlatform != null && firstPlatform.transform.position.y > player.position.y + despawnDistance)
            {
                Destroy(firstPlatform);
                spawnedPlatforms.RemoveAt(0);
            }
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefab == null)
        {
            Debug.LogWarning("Platform prefab is not assigned in PlatformSpawner.");
            return;
        }

        float yRotation = Random.Range(0f, 360f);
        Vector3 spawnPosition = new Vector3(0f, lastY, 0f);

        GameObject newPlatform = Instantiate(
            platformPrefab,
            spawnPosition,
            Quaternion.Euler(0f, yRotation, 0f)
        );

        spawnedPlatforms.Add(newPlatform);
        lastY -= verticalSpacing;
    }
}