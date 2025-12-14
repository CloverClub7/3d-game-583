using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 2;

    // SFX
    public AudioClip fireSound;
    public AudioClip hitBody1;
    public AudioClip hitBody2;
    public AudioClip hitConcrete1;
    public AudioClip hitConcrete2;
    public AudioClip hitMetal1;
    public AudioClip hitMetal2;
    public float soundVolume = 1f;

    void Start()
    {
        // Destroy the bullet after a set lifetime to avoid clutter
        Destroy(gameObject, lifeTime);

        // Play fire sound
        SoundFXManager.instance.PlaySoundClip(fireSound, transform, soundVolume);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we hit something tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit! Remaining HP: " + enemy.health);
            }

            // Play body hit sound
            int selectSound = Random.Range(0, 2);
            if (selectSound == 0)
            {
                SoundFXManager.instance.PlaySoundClip(hitBody1, transform, soundVolume);
            }
            else
            {
                SoundFXManager.instance.PlaySoundClip(hitBody2, transform, soundVolume);
            }
        }

        else
        {
            // Play concrete hit sound
            int selectSound = Random.Range(0, 2);
            if (selectSound == 0)
            {
                SoundFXManager.instance.PlaySoundClip(hitConcrete1, transform, soundVolume);
            }
            else
            {
                SoundFXManager.instance.PlaySoundClip(hitConcrete2, transform, soundVolume);
            }            
        }

        // Destroy the bullet on any collision
        Destroy(gameObject);
    }
}
