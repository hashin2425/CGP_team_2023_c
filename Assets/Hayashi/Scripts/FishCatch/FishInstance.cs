using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;
using UnityEditorInternal;

public class FishInstance : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public float moveSpeed = 1.0f;
    public GameObject ripple;
    public string catcherName = "Catcher";
    public string bucketName = "Bucket";

    private Rigidbody2D rb;
    private System.Random random = new System.Random();
    private float moving_target_X = 0;
    private float moving_target_Y = 0;
    private string movingStatus = "wait";
    private float degStopRotation = 0.25f;
    private float distanceStopMoving = 0.5f;
    private float distanceShowRipple = 8.0f;
    private Vector2 direction;
    public bool isCaught = false;
    private GameObject catcher;
    private Bounds bucketBounds;
    private Collider2D bucketCollider;
    private Vector3 catcherLastPosition = Vector2.zero;
    private bool isInBucket = false;

    bool is_in_frame()
    {
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 10 || transform.position.y < -10)
        {
            return false;
        }
        return true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Do not run heavy calculations in OnTriggerStay2D, as it is called every frame
        if (collision.gameObject.tag == "Catcher_sunk" && !isInBucket)
        {
            isCaught = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.5f);
        }
        if (collision.gameObject.tag == "Bucket" && isCaught)
        {
            Console.WriteLine("Fish in bucket");
            isInBucket = true;
            isCaught = false;

            // Move fish in front of bucket(change its x position)
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, -0.1f);
            transform.position = newPosition;

            // omit tag
            gameObject.tag = "CaughtFish";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Catcher" || collision.gameObject.tag == "Catcher_sunk")
        {
            isCaught = false;
        }

    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        catcher = GameObject.Find(catcherName);
        GameObject bucket = GameObject.Find(bucketName);
        bucketCollider = bucket.GetComponent<Collider2D>();
        bucketBounds = bucketCollider.bounds;

    }

    private void FixedUpdate()
    {
        if (isCaught)
        {
            if (catcherLastPosition == Vector3.zero)
            {
                catcherLastPosition = catcher.transform.position;
                rb.velocity = Vector2.zero;
            }
            Vector3 deltaPosition = catcher.transform.position - catcherLastPosition;
            transform.position += deltaPosition;
            catcherLastPosition = catcher.transform.position;

            movingStatus = "wait";
        }
        else
        {
            catcherLastPosition = Vector3.zero;
            if (!is_in_frame())
            {
                Destroy(gameObject);
            }
            if (movingStatus == "wait")
            {
                if (isInBucket)
                {
                    Vector2 randomPosition;
                    do
                    {
                        moving_target_X = random.Next((int)bucketBounds.min.x, (int)bucketBounds.max.x);
                        moving_target_Y = random.Next((int)bucketBounds.min.y, (int)bucketBounds.max.y);
                        randomPosition = new Vector2(moving_target_X, moving_target_Y);
                    } while (!bucketCollider.OverlapPoint(randomPosition));
                }
                else
                {
                    moving_target_X = random.Next(-10, 10) * 0.75f;
                    moving_target_Y = random.Next(-10, 10) * 0.75f;
                }
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
                    if (Vector2.Distance(transform.position, new Vector2(moving_target_X, moving_target_Y)) > distanceShowRipple)
                    {
                        Instantiate(ripple, transform.position, Quaternion.identity);
                    }
                }
            }
            if (movingStatus == "swim")
            {
                rb.velocity = new Vector2(moving_target_X, moving_target_Y) - (Vector2)transform.position;
                if (Vector2.Distance(transform.position, new Vector2(moving_target_X, moving_target_Y)) < distanceStopMoving)
                {
                    rb.velocity = Vector2.zero;
                    movingStatus = "wait";
                }
            }
        }
    }
}
