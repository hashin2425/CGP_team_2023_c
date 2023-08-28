using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using System.Threading.Tasks;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;
    public int maxFishNum = 100; //最大金魚数

    public Transform[] spawnPoints;

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Fish").Length < maxFishNum)
        {
            int randomSpawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
            GameObject newFish = Instantiate(fish, spawnPosition, Quaternion.identity);
        }
    }
}
