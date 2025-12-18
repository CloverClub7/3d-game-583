using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("UI")]
    public GameObject scoreText;
    private ScoreCounter scoreCounter;

    [Header("Health")]
    public int health = 4;

    [Header("Spawner")]
    public ZombieSpawner spawnedFrom;

    [Header("Sounds")]
    public AudioClip zombieHurt1;
    public AudioClip zombieHurt2;
    public AudioClip zombieHurt3;
    public AudioClip zombieHurt4;
    public AudioClip zombieHurt5;
    public AudioClip zombieHurt6;
    public float soundVolume = 1f;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage. Remaining Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void PlaySound()
    {
        switch(Random.Range(0,6))
        {
            case 0:
                SoundFXManager.instance.PlaySoundClip(zombieHurt1, transform, soundVolume);
                break;
            case 1:
                SoundFXManager.instance.PlaySoundClip(zombieHurt2, transform, soundVolume);
                break;
            case 2:
                SoundFXManager.instance.PlaySoundClip(zombieHurt3, transform, soundVolume);
                break;
            case 3:
                SoundFXManager.instance.PlaySoundClip(zombieHurt4, transform, soundVolume);
                break;
            case 4:
                SoundFXManager.instance.PlaySoundClip(zombieHurt5, transform, soundVolume);
                break;
            case 5:
                SoundFXManager.instance.PlaySoundClip(zombieHurt6, transform, soundVolume);
                break;
            default:
                break;
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        scoreCounter.UpdateScore();
        PlaySound();
        spawnedFrom.zombieCount--;
        Destroy(gameObject);
    }

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score");
        scoreCounter = scoreText.GetComponent<ScoreCounter>();
    }
}
