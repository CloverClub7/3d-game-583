using UnityEngine;

// Creates sound objects to play sound, so that if an enemy dies the full sound clip can still be played
public class SoundFXManager : MonoBehaviour
{
    public AudioSource soundFXObject;
    public static SoundFXManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Play a sound
    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
