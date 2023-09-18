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
    public float spawnIntervalSec = 0;
    public int randomRotateRange = 0;

    private Transform[] spawnPoints;
    private float spawnIntervalRemainSec = 0;
    private System.Random random = new System.Random();

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
        spawnIntervalRemainSec -= Time.deltaTime;
        if (GameObject.FindGameObjectsWithTag(managedTag).Length < maxObjectCount && spawnIntervalRemainSec < 0)
        {
            spawnIntervalRemainSec = spawnIntervalSec;
            int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
            GameObject newObject = Instantiate(obj, spawnPosition, Quaternion.identity);
            if (randomRotateRange > 0)
            {
                newObject.GetComponent<Rigidbody2D>().AddTorque(random.Next(-randomRotateRange, randomRotateRange));
            }
        }
    }
}
