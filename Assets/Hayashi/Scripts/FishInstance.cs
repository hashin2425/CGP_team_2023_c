using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

public class FishInstance : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 1.0f;
    public GameObject ripple;

    private Rigidbody2D rb;
    private System.Random random = new System.Random();
    private float moving_target_X = 0;
    private float moving_target_Y = 0;
    private string movingStatus = "wait";
    private float degStopRotation = 0.25f;
    private float distanceStopMoveing = 0.5f;
    private float dictanceShowRipple = 8.0f;
    private Vector2 direction;

    public void Catched() { }

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
        if (movingStatus == "wait")
        {
            moving_target_X = random.Next(-10, 10) * 0.75f;
            moving_target_Y = random.Next(-10, 10) * 0.75f;
            movingStatus = "rotate";
        }
        if (movingStatus == "rotate")
        {
            direction = new Vector2(moving_target_X, moving_target_Y) - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotation) < degStopRotation)
            {
                movingStatus = "swim";
                if (Vector2.Distance(transform.position, new Vector2(moving_target_X, moving_target_Y)) > dictanceShowRipple)
                {
                    Instantiate(ripple, transform.position, Quaternion.identity);
                }
            }
        }
        if (movingStatus == "swim")
        {
            rb.velocity = new Vector2(moving_target_X, moving_target_Y) - (Vector2)transform.position;
            if (Vector2.Distance(transform.position, new Vector2(moving_target_X, moving_target_Y)) < distanceStopMoveing)
            {
                rb.velocity = Vector2.zero;
                movingStatus = "wait";
            }
        }
    }
}