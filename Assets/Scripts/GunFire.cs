using UnityEngine;

public class GunFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletForce = 700f;       // Force applied to bullet
    public int maxAmmo = 6;
    private int currentAmmo;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Clear any constraints so bullet can move freely
            rb.constraints = RigidbodyConstraints.None;

            // Ensure no gravity so bullet flies straight
            rb.useGravity = false;

            // Set collision detection mode for better hit detection at high speed
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // Launch bullet forward
            rb.AddForce(bulletSpawnPoint.forward * bulletForce, ForceMode.Impulse);

            Debug.Log("Bullet launched with force: " + bulletForce);
        }

        currentAmmo--;
        Debug.Log("Fired! Ammo left: " + currentAmmo);
    }
}
