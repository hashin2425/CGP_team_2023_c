using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public class FishInstance : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 1.0f;

    private Rigidbody2D rb;
    private System.Random random = new System.Random();
    private float moving_target_X = 0;
    private float moving_target_Y = 0;
    private string moving_status = "wait";
    private float rotate_stop = 0.25f;
    private float move_stop = 0.5f;
    private Vector2 direction;

    bool is_in_frame()
    {
        if (transform.position.x > 8 || transform.position.x < -8 || transform.position.y > 6 || transform.position.y < -6)
        {
            return false;
        }
        return true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!is_in_frame())
        {
            Destroy(gameObject);
        }
        if (moving_status == "wait")
        {
            moving_target_X = random.Next(-10, 10) * 0.75f;
            moving_target_Y = random.Next(-10, 10) * 0.75f;
            moving_status = "rotate";
        }
        if (moving_status == "rotate")
        {
            direction = new Vector2(moving_target_X, moving_target_Y) - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotation) < rotate_stop)
            {
                moving_status = "swim";
            }
        }
        if (moving_status == "swim")
        {
            rb.velocity = new Vector2(moving_target_X, moving_target_Y) - (Vector2)transform.position;
            if (Vector2.Distance(transform.position, new Vector2(moving_target_X, moving_target_Y)) < move_stop)
            {
                rb.velocity = Vector2.zero;
                moving_status = "wait";
            }
        }
    }
}