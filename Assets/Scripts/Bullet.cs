using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int damage = 2;

    void Start()
    {
        // Destroy the bullet after a set lifetime to avoid clutter
        Destroy(gameObject, lifeTime);
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
        }

        // Destroy the bullet on any collision
        Destroy(gameObject);
    }
}
