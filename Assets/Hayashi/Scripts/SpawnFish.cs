using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using System.Threading.Tasks;

public class SpawnFish : MonoBehaviour
{
    public GameObject fish;

    void FixedUpdate()
    {
        if (GameObject.FindGameObjectsWithTag("Fish").Length < 5)
        {
            for (int i = 0; i < 15; i++)
            {
                Instantiate(fish, transform.position, Quaternion.identity);
            }
        }
    }
}
