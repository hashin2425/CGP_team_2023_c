using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatcherScript : MonoBehaviour
{
    public TimeManagement timeManagement;
    public GameObject HPBarObject;
    public Slider HPBar;
    public Image fillImage;
    public TextMeshProUGUI fishConutText;

    private Vector3 mousePos, worldPos;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;
    private Vector2 lastVelocity;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        lastVelocity = rb.velocity;
        fishConutText.text = "0";
    }

    void FixedUpdate()
    {
        if (timeManagement.timeLeft <= 0) 
        {
            return;
        }

        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // Since the layers are divided by the Z coordinate, they are not considered in the calculation of the movement speed
        Vector2 currentVelocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
        float acceleration = ((currentVelocity - lastVelocity) / Time.deltaTime).magnitude;
        lastPosition = transform.position;
        lastVelocity = currentVelocity;

        if (Input.GetMouseButton(0))
        {
            // on mouse click
            worldPos.z = 2.0f;
            spriteRenderer.color = Color.gray;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.0f);
            gameObject.tag = "Catcher_sunk";

            HPBarObject.SetActive(true);
            Vector3 HPBarPos = gameObject.transform.position;
            HPBarPos.y += 2.0f; // not to overlap with the catcher
            HPBarPos.z = 0;
            HPBar.transform.position = Camera.main.WorldToScreenPoint(HPBarPos);
            if (acceleration > 1)
            {
                HPBar.value += Mathf.Clamp01(acceleration / 3000);
            }
            HPBar.value -= 0.0005f / Time.deltaTime;
            if (HPBar.value >= HPBar.maxValue * 0.7f)
            {
                fillImage.color = Color.red;
            }
            else if (HPBar.value >= HPBar.maxValue * 0.4f)
            {
                fillImage.color = Color.yellow;
            }
            else
            {
                fillImage.color = Color.green;
            }
        }
        else
        {
            // on mouse released
            worldPos.z = -1.0f;
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");

            foreach (GameObject obj in objects)
            {
                FishInstance script = obj.GetComponent<FishInstance>();
                script.isCaught = false;
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0.0f);
            }

            spriteRenderer.color = Color.white;
            gameObject.tag = "Catcher";

            HPBar.value = 0;
            HPBarObject.SetActive(false);

            //
            fishConutText.text = GameObject.FindGameObjectsWithTag("CaughtFish").Length.ToString();
        }
        // follow the mouse
        float newX = Mathf.Clamp(worldPos.x, -9, 9);
        float newY = Mathf.Clamp(worldPos.y, -5, 5);
        transform.position = new Vector3(newX, newY, worldPos.z);
    }

}