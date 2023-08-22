using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldfishNumber : MonoBehaviour
{
    public GameObject fishPrefab;
    public int maxFishNum = 100; //最大金魚数
    public Transform[] spawnPoints; //出現座標配列
    // Start is called before the first frame update
    void Start()
    {
        SpawnFish();
    }

    // Update is called once per frame
    void Update()
    {
        int currentFishCount = GameObject.FindGameObjectsWithTag("Fish").Length;
        if (currentFishCount < maxFishNum)
        {
            SpawnFish();
        }
    }
    private void SpawnFish()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomSpawnIndex].position;
        GameObject newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }
}
