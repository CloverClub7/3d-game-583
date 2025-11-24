using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 4;

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
        Destroy(gameObject);
    }
}
