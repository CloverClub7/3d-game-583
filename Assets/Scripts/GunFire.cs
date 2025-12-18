using UnityEngine;
using System.Collections;

public class GunFire : MonoBehaviour
{
    [Header("Visual Bullet (optional)")]
    public GameObject bulletPrefab;          // leave null if you do not want a visible bullet
    public Transform bulletSpawnPoint;       // usually an empty at the muzzle
    public float bulletForce = 700f;

    [Header("Aiming")]
    public Camera fpsCamera;                 // drag FirstPersonCamera here
    public float maxShootDistance = 100f;
    public LayerMask hitLayers = ~0;         // what the raycast can hit
    public bool debugRay = false;

    [Header("Ammo Settings")]
    public int maxAmmo = 6;
    public float reloadTime = 1.5f;
    public int damage = 2;

    [Header("Fire Rate")]
    public float timeBetweenShots = 0.5f;    // seconds between shots, higher = slower fire rate

    [Header("UI")]
    public AmmoUI ammoUI;                    // drag your AmmoUI object here in Inspector

    private int currentAmmo;
    private bool isReloading = false;
    private float nextShotTime = 0f;

    [Header("SFX")]
    public AudioClip reloadSound;
    private AudioSource gunAudioSource;

    private Animator animator;

    void Start()
    {
        currentAmmo = maxAmmo;

        if (ammoUI != null)
        {
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }

        gunAudioSource = GetComponent<AudioSource>();

        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Left click to fire, but only if cooldown is finished
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextShotTime)
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    void Fire()
    {
        // 1. Decide where we are aiming from the camera
        Transform view = fpsCamera != null ? fpsCamera.transform : transform;
        Vector3 origin = view.position;
        Vector3 direction = view.forward;

        if (debugRay)
        {
            Debug.DrawRay(origin, direction * maxShootDistance, Color.red, 0.25f);
        }

        Vector3 targetPoint;

        // 2. Raycast from camera through the reticle
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxShootDistance, hitLayers, QueryTriggerInteraction.Ignore))
        {
            targetPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);
            // Later we will put zombie damage here
        }
        else
        {
            targetPoint = origin + direction * maxShootDistance;
        }

        // 3. Optional visible bullet from the gun toward the target point
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.damage = damage;

            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.useGravity = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

                Vector3 bulletDir = (targetPoint - bulletSpawnPoint.position).normalized;
                rb.AddForce(bulletDir * bulletForce, ForceMode.Impulse);

                Debug.Log("Bullet launched with force: " + bulletForce);
            }
        }

        // 4. Ammo and cooldown
        currentAmmo--;
        Debug.Log("Fired! Ammo left: " + currentAmmo);

        nextShotTime = Time.time + timeBetweenShots;

        if (ammoUI != null)
        {
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("isReloading", isReloading);
        Debug.Log("Reloading...");

        // Play reload sound, must follow gun so cannot use SoundFXManager
        gunAudioSource.clip = reloadSound;
        gunAudioSource.volume = 1f;
        gunAudioSource.Play();

        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = maxAmmo;
        isReloading = false;
        animator.SetBool("isReloading", isReloading);

        Debug.Log("Reload Complete! Ammo refilled to " + currentAmmo);

        if (ammoUI != null)
        {
            ammoUI.UpdateAmmoDisplay(currentAmmo);
        }
    }
}
