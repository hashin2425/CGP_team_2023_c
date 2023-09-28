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
    public TextMeshProUGUI catcherCountText;
    public Sprite catcherBroken;
    public Sprite catcherBrokenUnderWater;
    public Sprite catcherNormal;
    public Sprite catcherNormalUnderWater;
    public Sprite catcherAllBroken;
    public Animator fishCountAnimator;
    public int catcherRemainCount;
    public GameObject destroyParticle;
    public AudioClip seIntoWater;
    public AudioClip seOutWater;
    public AudioClip seBrokenPartly;
    public AudioClip seBrokenCritically;
    public float catcherReloadInterval = 1;
    public PauseMenuScript PauseMenuScript;

    private AudioSource audioSource;
    private Vector3 mousePos, worldPos;
    private SpriteRenderer spriteRenderer;
    private Vector2 lastPosition;
    private Vector2 lastVelocity;
    private Rigidbody2D rb;
    private bool isPartiallyTorn = false;
    private bool isTotallyTorn = false;
    private bool isBeforeCatcherInWater = false;
    private float catcherStrength = 2000;
    private float catcherReloadIntervalRemainTime = 0;
    private int howManyFishOnCatcher = 0;

    private bool getIsMouseClicked()
    {
        bool result = Input.GetMouseButton(0) && PauseMenuScript.canMouseBeClicked;
        return result;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        lastVelocity = rb.velocity;
        fishConutText.text = "0";
        catcherCountText.text = catcherRemainCount.ToString();
    }

    void FixedUpdate()
    {
        if (timeManagement.timeLeft <= 0 || timeManagement.isGameEnd)
        {
            HPBarObject.SetActive(false);
            return;
        }
        if (catcherReloadIntervalRemainTime > 0)
        {
            catcherReloadIntervalRemainTime -= Time.deltaTime;
            return;
        }

        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));

        // Since the layers are divided by the Z coordinate, they are not considered in the calculation of the movement speed
        Vector2 currentVelocity = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
        float acceleration = ((currentVelocity - lastVelocity) / Time.deltaTime).magnitude;
        lastPosition = transform.position;
        lastVelocity = currentVelocity;

        if (getIsMouseClicked() && !isTotallyTorn)
        {
            // on mouse click
            if (!isBeforeCatcherInWater)
            {
                audioSource.PlayOneShot(seIntoWater);
            }
            worldPos.z = 2.0f;

            // change skin
            if (isPartiallyTorn && !isTotallyTorn)
            {
                spriteRenderer.sprite = catcherBrokenUnderWater;
            }
            else if (!isPartiallyTorn && !isTotallyTorn)
            {
                spriteRenderer.sprite = catcherNormalUnderWater;
            }

            // set all fish released
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");
            foreach (GameObject obj in objects)
            {
                FishInstance script = obj.GetComponent<FishInstance>();
                script.OnCatcherIntoWater();
                howManyFishOnCatcher = 0;
            }

            // move
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.0f);

            // change tag
            gameObject.tag = "Catcher_sunk";

            // HP bar
            HPBarObject.SetActive(true);
            Vector3 HPBarPos = gameObject.transform.position;
            HPBarPos.y += 2.0f; // not to overlap with the catcher
            HPBarPos.z = 0;
            HPBar.transform.position = Camera.main.WorldToScreenPoint(HPBarPos);
            if (acceleration > 1)
            {
                HPBar.value += Mathf.Clamp01(acceleration / catcherStrength);
            }
            HPBar.value -= 0.0005f / Time.deltaTime;
            if (HPBar.value >= HPBar.maxValue * 0.7f)
            {
                fillImage.color = Color.red;
                if (isPartiallyTorn)
                {
                    // 「部分的に破れた」の処理
                    gameObject.tag = "Catcher";

                    spriteRenderer.sprite = catcherAllBroken;
                    destroyParticle.SetActive(false);
                    destroyParticle.SetActive(true);
                    isTotallyTorn = true;
                    audioSource.PlayOneShot(seBrokenPartly);

                    // set all fish released
                    foreach (GameObject obj in objects)
                    {
                        FishInstance script = obj.GetComponent<FishInstance>();
                        script.OnCatcherIntoWater();
                        howManyFishOnCatcher = 0;
                    }
                }
                else
                {
                    // 「完全に破れた」の処理
                    isPartiallyTorn = true;
                    HPBar.value = 0;
                    audioSource.PlayOneShot(seBrokenCritically);

                    // set all fish released
                    foreach (GameObject obj in objects)
                    {
                        FishInstance script = obj.GetComponent<FishInstance>();
                        script.OnCatcherIntoWater();
                        howManyFishOnCatcher = 0;
                    }
                }
            }
            else if (HPBar.value >= HPBar.maxValue * 0.4f)
            {
                fillImage.color = Color.yellow;
            }
            else
            {
                fillImage.color = Color.green;
            }

            isBeforeCatcherInWater = true;
        }
        else if (getIsMouseClicked() && isTotallyTorn)
        {
            if (catcherRemainCount > 0)
            {
                catcherRemainCount--;
                catcherCountText.text = catcherRemainCount.ToString();
                catcherReloadIntervalRemainTime = catcherReloadInterval;
                HPBarObject.SetActive(false);
                isTotallyTorn = false;
                isPartiallyTorn = false;
            }
            else
            {
                timeManagement.isGameEnd = true;
            }
        }
        else
        {
            // on mouse released
            worldPos.z = -1.0f;
            if (isBeforeCatcherInWater)
            {
                audioSource.PlayOneShot(seOutWater);
            }

            GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");
            if (isBeforeCatcherInWater)
            {
                foreach (GameObject obj in objects)
                {
                    FishInstance script = obj.GetComponent<FishInstance>();
                    howManyFishOnCatcher += script.OnCatcherLiftedOutWater(acceleration);
                }

                if (howManyFishOnCatcher >= 3)
                {
                    // ポイに金魚を載せすぎると破れる

                    // 「完全に破れた」の処理
                    isPartiallyTorn = true;
                    HPBar.value = 0;
                    audioSource.PlayOneShot(seBrokenCritically);

                    // set all fish released
                    foreach (GameObject obj in objects)
                    {
                        FishInstance script = obj.GetComponent<FishInstance>();
                        script.OnCatcherIntoWater();
                        howManyFishOnCatcher = 0;
                    }
                }
            }

            // change skin
            if (isPartiallyTorn && !isTotallyTorn)
            {
                spriteRenderer.sprite = catcherBroken;
            }
            else if (!isTotallyTorn && !isTotallyTorn)
            {
                spriteRenderer.sprite = catcherNormal;
            }

            // change tag
            gameObject.tag = "Catcher";

            // set HP bar
            HPBar.value = 0;
            HPBarObject.SetActive(false);

            // update score
            string newFishCountText = GameObject.FindGameObjectsWithTag("CaughtFish").Length.ToString();
            if (fishConutText.text != newFishCountText)
            {
                fishConutText.text = newFishCountText;
                fishCountAnimator.Play("TextOnChanged", 0, 0);
            }

            isBeforeCatcherInWater = false;
        }
        // follow the mouse
        float newX = Mathf.Clamp(worldPos.x, -9, 9);
        float newY = Mathf.Clamp(worldPos.y, -5, 5);
        transform.position = new Vector3(newX, newY, worldPos.z);
    }
}