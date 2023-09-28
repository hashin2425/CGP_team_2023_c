using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erasepotato : MonoBehaviour
{
    private bool isCaught = false;
    private float startTime;
    private float journeyLength;
    private Vector3 endPosition = new Vector3(-5.5f, 3.2f, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ext")
        {
            Destroy(gameObject);
        }
    }

    public void OnCaught()
    {
        isCaught = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        gameObject.transform.localScale = Vector3.one / 1.5f;
        startTime = Time.time;
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        journeyLength = Vector3.Distance(gameObject.transform.position, endPosition);

        Destroy(gameObject, 1);
        gameObject.tag = "Untagged";
    }

    private void FixedUpdate()
    {
        if (isCaught)
        {
            float distCovered = (Time.time - startTime) * 2;
            float moveProgress = distCovered / journeyLength;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, endPosition, moveProgress);
        }
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z + 45);
    }
}
