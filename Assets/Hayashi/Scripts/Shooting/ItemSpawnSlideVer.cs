using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnSlideVer : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval;

    private float passedTimeLastSpawn = 0f;
    private Transform[] spawnPoints;

    void Start()
    {
        List<Transform> spawnPointsList = new List<Transform>();
        foreach (Transform child in transform)
        {
            spawnPointsList.Add(child.transform);
        }
        spawnPoints = spawnPointsList.ToArray();
    }

    void FixedUpdate()
    {
        passedTimeLastSpawn += Time.deltaTime;

        if (passedTimeLastSpawn > spawnInterval)
        {
            int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
            GameObject newItem = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

            ShootingItem script = newItem.GetComponent<ShootingItem>();
            script.isThrown = false;

            passedTimeLastSpawn = 0f;
        }
    }
}