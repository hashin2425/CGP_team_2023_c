using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class baketsucontroller : MonoBehaviour
{

    public TextMeshProUGUI potatoScoreText;
    public float SPEED = 1.0f;
    public GameObject HowToControlPopUP;
    public TimeManagement timeManagement;
    public AudioClip seOnCatch;

    private Rigidbody2D rigidBody;
    private int potatoScore;
    private Vector2 inputAxis;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        potatoScoreText.text = potatoScore.ToString();
    }

    void FixedUpdate()
    {
        if (timeManagement.timeLeft >= 0)
        {
            if (gameObject.transform.position.x < -8 || gameObject.transform.position.x > 8)
            {
                inputAxis.x = 0;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow) && gameObject.transform.position.x < 8)
            {
                HowToControlPopUP.SetActive(false);
                inputAxis.x = 1;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, -10);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) && gameObject.transform.position.x > -8)
            {
                HowToControlPopUP.SetActive(false);
                inputAxis.x = -1;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 10);
            }
            rigidBody.velocity = inputAxis.normalized * SPEED;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("potato"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(seOnCatch);
            other.gameObject.GetComponent<erasepotato>().OnCaught();
            potatoScore++;
            potatoScoreText.text = potatoScore.ToString();
        }
    }
}
