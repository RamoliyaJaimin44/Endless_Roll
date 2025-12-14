using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public List<GameObject> enemyPrefabs;
    public GameObject bulletPickupPrefab;
    public GameObject speedPickupPrefab;
    public Transform spawnParent;
    private PlayerController playerController;

    [Header("Spawn Settings")]
    public float spawnAheadDistance = 50f;
    public float spawnInterval = 1.5f;
    public float spawnHeight = 0.5f;

    [Header("Lane Settings")]
    public List<float> lanes;

    [Header("Cleanup")]
    public float destroyBehindDistance = 10f;

    private List<GameObject> allSpawnedObjects = new List<GameObject>();

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        CleanupOldObjects();
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (playerController != null && playerController.IsMoving())
            {
                SpawnRandomObject();
            }
        }
    }

    void SpawnRandomObject()
    {
        GameObject prefabToSpawn = ChooseRandomPrefab();

        if (prefabToSpawn != null)
        {
            Vector3 spawnPos = GetRandomLanePosition();
            GameObject spawned = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, spawnParent);
            allSpawnedObjects.Add(spawned);
        }
    }

    GameObject ChooseRandomPrefab()
    {
        int random = Random.Range(0, 10);

        if (random < 6)
        {
            return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        }
        else if (random < 8)
        {
            return bulletPickupPrefab;
        }
        else
        {
            return speedPickupPrefab;
        }
    }

    Vector3 GetRandomLanePosition()
    {
        float randomLane = lanes[Random.Range(0, lanes.Count)];
        float spawnZ = player.position.z + spawnAheadDistance;

        return new Vector3(randomLane, spawnHeight, spawnZ);
    }

    void CleanupOldObjects()
    {
        for (int i = allSpawnedObjects.Count - 1; i >= 0; i--)
        {
            if (allSpawnedObjects[i] == null)
            {
                allSpawnedObjects.RemoveAt(i);
            }
            else if (allSpawnedObjects[i].transform.position.z < player.position.z - destroyBehindDistance)
            {
                Destroy(allSpawnedObjects[i]);
                allSpawnedObjects.RemoveAt(i);
            }
        }
    }
}
