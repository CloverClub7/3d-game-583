using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // The zombie prefab
    public List<GameObject> zombies = new List<GameObject>(); // Public so other scripts can access
    public float difficultyInterval = 120f; // Time between difficulty change

    private float zombieSpawnDelayMin = 5f; // Minumum time between zombie spawns
    private float zombieSpawnDelayMax = 20f; // Maximum time between zombie spawns
    private float maxZombies = 100f; // Maximum amount of zombies that can be on the scene
    private float timeLastDifficulty = 0f; // Time since last difficulty change

    // Spawn a zombie
    void SpawnZombie()
    {
        // If maxZombies is reached, do not spawn another zombie
        if (zombies.Count < maxZombies)
        {
            GameObject zombie = Instantiate(zombiePrefab);
            zombie.transform.position = transform.position;
            zombies.Add(zombie);
        }

        // Change the spawn interval based on an interval
        if (Time.time - timeLastDifficulty > difficultyInterval)
        {
            timeLastDifficulty = Time.time;
            zombieSpawnDelayMin /= 3f;
            zombieSpawnDelayMax /= 3f;
        }

        Invoke("SpawnZombie", Random.Range(zombieSpawnDelayMin, zombieSpawnDelayMax));
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnZombie", 5f);
    }
}
