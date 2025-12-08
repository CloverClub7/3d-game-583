using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float damage = 20f;
    public float damageCooldown = 1f;

    private float lastDamageTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}
