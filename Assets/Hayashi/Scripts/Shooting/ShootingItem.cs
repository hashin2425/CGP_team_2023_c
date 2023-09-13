using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    public int[] childrenPoints;
    public bool isThrown = true;
    public GameObject destroyParticle;
    public int currentPoint;


    private float thrownSpeed = 7.5f;
    private float slideSpeed = 7.5f;
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

        if (isThrown)
        {
            rb.velocity = new Vector2(Mathf.Abs(gameObject.transform.position.x) / gameObject.transform.position.x * thrownSpeed * -1, 7.5f);
        }
        else
        {
            currentSkin.GetComponent<PolygonCollider2D>().isTrigger = false;
            rb.velocity = new Vector2(Mathf.Abs(gameObject.transform.position.x) / gameObject.transform.position.x * slideSpeed * -1, 0);
            rb.gravityScale = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    bool is_in_frame()
    {
        if (transform.position.x > 20 || transform.position.x < -20 || transform.position.y > 16 || transform.position.y < -16)
        {
            return false;
        }
        return true;
    }

    public void destroyMySelf()
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
    }
}