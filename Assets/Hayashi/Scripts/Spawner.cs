using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using System.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    public GameObject obj;
    public int maxObjectCount = 100; //最大オブジェクト数
    public string managedTag;

    private Transform[] spawnPoints;

    void Start()
    {
        List<Transform> spawnPointsList = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                spawnPointsList.Add(child.transform);
            }
        }
        spawnPoints = spawnPointsList.ToArray();
    }

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag(managedTag).Length < maxObjectCount)
        {
            int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
            GameObject newObject = Instantiate(obj, spawnPosition, Quaternion.identity);
        }
    }
}
