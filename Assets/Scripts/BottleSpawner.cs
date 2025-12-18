using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawner : MonoBehaviour
{
    public GameObject bottlePrefab;
    public float bottleSpawnDelayMin = 10f; // Minumum time between bottle spawns
    public float bottleSpawnDelayMax = 60f; // Maximum time between bottle spawns
    public int bottleCount = 0;

    // Spawn a bottle
    void SpawnBottle()
    {
        // If there is already a bottle, do not spawn another
        if (bottleCount < 1)
        {
            GameObject bottle = Instantiate(bottlePrefab);
            bottle.transform.position = transform.position;
            Bottles bottleComponent = bottle.GetComponentInChildren<Bottles>();

            // Spawn a random bottle out of the 3 selections
            bottleComponent.bottleType = Random.Range(0,3);
            bottleComponent.ChangeColour();
            bottleComponent.spawnedFrom = this;
            bottleCount++;
        }

        Invoke("SpawnBottle", Random.Range(bottleSpawnDelayMin, bottleSpawnDelayMax));
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnBottle", bottleSpawnDelayMin);
    }
}
