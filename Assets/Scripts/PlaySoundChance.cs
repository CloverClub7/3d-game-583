using UnityEngine;

public class PlaySoundChance : MonoBehaviour
{
    public AudioClip audioClip;
    public float interval = 180f; // Interval between attempts to play the sound
    public float chance = 0.067f; // Chance of the sound being played per attempt
    public float volume = 1f;
    
    private AudioSource audioSource;
    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.volume = volume;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            timer = 0f;
            PlaySoundAttempt();
        }
    }

    void PlaySoundAttempt()
    {
        float playSound = Random.Range(0, 101);
        if (playSound <= chance * 100)
        {
            audioSource.Play();
        }
    }
}