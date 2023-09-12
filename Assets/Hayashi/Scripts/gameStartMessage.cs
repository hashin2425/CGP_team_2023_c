using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartMessage : MonoBehaviour
{
    private float pos_x = 20;
    public float a = 0.015f;
    public float b = 0.15f;
    public float c = 0.15f;

    void FixedUpdate()
    {
        pos_x -= (float)Math.Pow(Math.Abs(gameObject.transform.position.x) * a, b) + c;
        Vector3 pos = new Vector3(pos_x, gameObject.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = pos;
        if (pos_x < -20)
        {
            Destroy(gameObject);
        }
    }
}
