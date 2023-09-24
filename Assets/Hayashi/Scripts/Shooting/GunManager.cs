using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject GunAnchor;
    public GameObject Reticle;
    public GameObject BulletPrefab;
    public GameObject Muzzle;
    public int bulletCount = 100;
    public float bulletSpeed = 10f;
    public float rotateAdjustmentDegree = 180f; // if gun image is needed to be rotated, set this value to 90f
    public float shootInterval = 0.5f;
    public TextMeshProUGUI bulletLeftText;
    public Animator bulletLeftAnimator;
    public TextMeshProUGUI pointText;
    public Animator pointAnimator;
    public TimeManagement timeManagement;
    public AudioClip seShoot;
    public AudioClip seReload;
    public AudioClip seHitNormal;
    public AudioClip seHitHigh;
    public AudioClip seHitSpecial;
    public PauseMenuScript PauseMenuScript;

    private AudioSource audioSource;
    private float passedTimeLastShoot = 0;
    private int playerScore = 0;
    private CircleCollider2D reticleCollider;
    private bool getIsMouseClicked()
    {
        bool result = Input.GetMouseButton(0) && PauseMenuScript.canMouseBeClicked;
        return result;
    }

    void Start()
    {
        reticleCollider = Reticle.GetComponent<CircleCollider2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0.075f;
    }

    void FixedUpdate()
    {
        if (timeManagement.timeLeft <= 0)
        {
            return;
        }
        Vector3 gunAnchorPos = GunAnchor.transform.position;
        Vector3 reticlePos = Reticle.transform.position;

        // move reticle to mouse position
        Vector3 rawMousePos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(rawMousePos.x, rawMousePos.y, 10f));
        Reticle.transform.position = new Vector3(
            Mathf.Clamp(mousePos.x, -8.65f, 8.65f),
            Mathf.Clamp(mousePos.y, -4.75f, 4.75f),
            mousePos.z
        );

        // rotate gun
        float dx = reticlePos.x - gunAnchorPos.x;
        float dy = reticlePos.y - gunAnchorPos.y;
        float newRad = Mathf.Atan2(dy, dx);
        float newDeg = newRad * Mathf.Rad2Deg;
        GunAnchor.transform.rotation = Quaternion.Euler(0, 0, newDeg + rotateAdjustmentDegree);

        // shoot bullet
        passedTimeLastShoot += Time.deltaTime;
        bool canShoot = getIsMouseClicked() && passedTimeLastShoot > shootInterval && bulletCount > 0;
        if (canShoot)
        {
            audioSource.PlayOneShot(seShoot);
            audioSource.clip = seReload;
            audioSource.PlayDelayed(shootInterval * 1.2f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(reticlePos, reticleCollider.radius);
            foreach (var collider in colliders)
            {
                if (collider != null && collider.gameObject.transform.parent != null)
                {
                    // Note: The parents object of child which has a collider have ShootingItem script.
                    if (collider.gameObject.transform.parent.tag == "ShootItem")
                    {
                        ShootingItem shootingItem = collider.gameObject.transform.parent.GetComponent<ShootingItem>();
                        audioSource.PlayOneShot(seHitNormal);
                        if (shootingItem != null)
                        {
                            playerScore += shootingItem.currentPoint;
                            if (shootingItem.currentPoint > 2)
                            {
                                audioSource.PlayOneShot(seHitHigh);
                            }
                            shootingItem.destroyMySelf();
                        }
                    }
                }
            }

            GameObject newBullet = Instantiate(BulletPrefab, Muzzle.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = GunAnchor.transform.right * bulletSpeed * -1;
            Destroy(newBullet, 5f); // destroy bullet automatically
            passedTimeLastShoot = 0;
            bulletCount--;
        }
        else if (bulletCount < 1)
        {
            timeManagement.isGameEnd = true;
        }

        // update UI
        string newPlayerScore = playerScore.ToString();
        if (pointText.text != newPlayerScore)
        {
            pointText.text = newPlayerScore;
            pointAnimator.Play("TextOnChanged", 0, 0);
        }
        string newBulletCount = bulletCount.ToString();
        if (bulletLeftText.text != newBulletCount)
        {
            bulletLeftText.text = newBulletCount;
            // bulletLeftAnimator.Play("TextOnChanged", 0, 0);
        }
    }
}
