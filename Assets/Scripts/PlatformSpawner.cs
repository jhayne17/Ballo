using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject platformPrefab;
    public Material redMaterial;
    public Material greenMaterial;
    public Transform player;
    public Transform tower;
    public Transform platformParent;

    [Header("Spawn Settings")]
    public int initialPlatforms = 12;
    public float verticalSpacing = 2f;
    public float spawnDistance = 20f;
    public float despawnDistance = 10f;

    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private float lastY = 0f;
    private bool isFirstPlatform = true;

    void Start()
    {
        if (player != null)
        {
            lastY = player.position.y - 2f;
        }

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

        while (lastY > player.position.y - spawnDistance)
        {
            SpawnPlatform();
        }

        while (spawnedPlatforms.Count > 0 &&
               spawnedPlatforms[0] != null &&
               spawnedPlatforms[0].transform.position.y > player.position.y + despawnDistance)
        {
            Destroy(spawnedPlatforms[0]);
            spawnedPlatforms.RemoveAt(0);
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefab == null)
        {
            Debug.LogWarning("No platform prefab assigned in PlatformSpawner.");
            return;
        }

        Vector3 spawnPosition = new Vector3(0f, lastY, 0f);
        float yRotation = Random.Range(0f, 360f);
        Transform parent = platformParent != null ? platformParent : tower;

        GameObject newPlatform = Instantiate(
            platformPrefab,
            spawnPosition,
            Quaternion.Euler(0f, yRotation, 0f),
            parent
        );

        if (isFirstPlatform)
        {
            SetAllGreen(newPlatform);
            isFirstPlatform = false;
        }
        else
        {
            AssignSegmentColors(newPlatform);
        }

        spawnedPlatforms.Add(newPlatform);
        lastY -= verticalSpacing;
    }

    void AssignSegmentColors(GameObject platform)
    {
        MeshRenderer[] renderers = platform.GetComponentsInChildren<MeshRenderer>();

        if (renderers.Length == 0)
        {
            return;
        }

        int guaranteedGreenIndex = Random.Range(0, renderers.Length);

        for (int i = 0; i < renderers.Length; i++)
        {
            bool isSafe;

            if (i == guaranteedGreenIndex)
            {
                isSafe = true;
            }
            else
            {
                isSafe = Random.value > 0.4f;
            }

            renderers[i].material = isSafe ? greenMaterial : redMaterial;
            renderers[i].gameObject.tag = isSafe ? "Safe" : "Kill";
        }
    }

    void SetAllGreen(GameObject platform)
    {
        MeshRenderer[] renderers = platform.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer r in renderers)
        {
            r.material = greenMaterial;
            r.gameObject.tag = "Safe";
        }
    }
}