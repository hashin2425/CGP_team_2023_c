using System.Collections;
using System.Collections.Generic;
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

    private float passedTimeLastShoot = 0;
    private CircleCollider2D reticleCollider;

    void Start()
    {
        reticleCollider = Reticle.GetComponent<CircleCollider2D>();

        // Time.timeScale = 0.1f;
    }

    void FixedUpdate()
    {
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
        bool canShoot = Input.GetMouseButton(0) && passedTimeLastShoot > shootInterval && bulletCount > 0;
        if (canShoot)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(reticlePos, reticleCollider.radius);
            foreach (var collider in colliders)
            {
                // Note: The parents object of child which has a collider have ShootingItem script.
                if (collider.gameObject.transform.parent.tag == "ShootItem")
                {
                    ShootingItem shootingItem = collider.gameObject.transform.parent.GetComponent<ShootingItem>();
                    shootingItem.destroyMySelf();
                }
            }

            GameObject newBullet = Instantiate(BulletPrefab, Muzzle.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = GunAnchor.transform.right * bulletSpeed * -1;
            Destroy(newBullet, 5f); // destroy bullet automatically
            passedTimeLastShoot = 0;
            bulletCount--;
        }
    }
}
