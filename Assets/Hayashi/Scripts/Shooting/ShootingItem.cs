using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public int[] childrenPoints;
    public bool isThrown = true;
    public GameObject destroyParticle;

    private float thrownSpeed = 7.5f;
    private float slideSpeed = 7.5f;
    private int currentPoint;
    private GameObject currentSkin;
    private Rigidbody2D rb;

    private System.Random random = new System.Random();


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        List<GameObject> childrenSkins = new List<GameObject>();
        foreach (Transform child in transform)
        {
            childrenSkins.Add(child.gameObject);
        }

        if (childrenPoints.Length != childrenSkins.Count)
        {
            Debug.LogError("points and texture2Ds must have the same length");
        }

        int index = Random.Range(0, childrenPoints.Length);
        currentPoint = childrenPoints[index];
        currentSkin = childrenSkins[index];

        currentSkin.SetActive(true);
        // currentSkin.GetComponent<PolygonCollider2D>().isTrigger = false;

        if (isThrown)
        {
            rb.velocity = new Vector2(Mathf.Abs(gameObject.transform.position.x) / gameObject.transform.position.x * thrownSpeed * -1, 7.5f);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Abs(gameObject.transform.position.x) / gameObject.transform.position.x * slideSpeed * -1, 0);
        }
    }

    bool is_in_frame()
    {
        if (transform.position.x > 12 || transform.position.x < -12 || transform.position.y > 10 || transform.position.y < -10)
        {
            return false;
        }
        return true;
    }

    void destroyMySelf()
    {
        GameObject newDestroyParticle = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(newDestroyParticle, 0.5f);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (!is_in_frame())
        {
            destroyMySelf();
        }

        if (true || Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (currentSkin.GetComponent<PolygonCollider2D>().OverlapPoint(mousePos))
            {
                destroyMySelf();
            }
        }
    }
}