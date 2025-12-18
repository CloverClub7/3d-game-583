using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public GameObject footStepsAudioObject;
    private AudioSource footStepsComponent;

    void Start()
    {
        footStepsAudioObject.SetActive(false);
        footStepsComponent = footStepsAudioObject.GetComponent<AudioSource>();
        footStepsComponent.pitch = 1.25f;
    }

    void doFootsteps()
    {
        footStepsAudioObject.SetActive(true);
    }

    void stopFootsteps()
    {
        footStepsAudioObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            doFootsteps();
        }
        if (Input.GetKeyDown("s"))
        {
            doFootsteps();
        }

        if (Input.GetKeyUp("w"))
        {
            stopFootsteps();
        }
        if (Input.GetKeyUp("s"))
        {
            stopFootsteps();
        }

        if (Input.GetKeyDown("left shift"))
        {
            footStepsComponent.pitch *= 2;
        }
        if (Input.GetKeyUp("left shift"))
        {
            footStepsComponent.pitch = 1.25f;
        }
    }
}
