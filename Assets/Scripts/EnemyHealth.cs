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

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy took damage. Remaining Health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");
        scoreCounter.UpdateScore();
        spawnedFrom.zombieCount--;
        Destroy(gameObject);
    }

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score");
        scoreCounter = scoreText.GetComponent<ScoreCounter>();
    }
}
