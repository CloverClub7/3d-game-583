using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EnemyHealth))]
public class ZombieAI : MonoBehaviour
{
    [Header("References")]
    public Transform playerTransform;

    [Header("Movement Settings")]
    public float moveSpeed = 3.5f;

    [Header("Attack Settings")]
    public float attackRange = 2f;
    public int damageAmount = 1;
    public float attackCooldown = 1.5f;

    [Header("Sounds")]
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;

    private Rigidbody rb;
    private EnemyHealth enemyHealth;
    private PlayerHealth playerHealth;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyHealth = GetComponent<EnemyHealth>();

        // Auto-assign playerTransform if not set
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found! Add 'Player' tag to the player GameObject.");
                enabled = false;
                return;
            }
        }

        // Cache PlayerHealth
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth component missing on player.");
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (enemyHealth.health <= 0 || playerTransform == null) return;

        MoveTowardsPlayer();

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            TryAttack();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0f;

        Vector3 move = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + move);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void TryAttack()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                PlaySound();
                Debug.Log("Zombie attacked player!");
            }

            lastAttackTime = Time.time;
        }
    }

    void PlaySound()
    {
        switch (Random.Range(0,3))
        {
            case 0:
                SoundFXManager.instance.PlaySoundClip(attack1, transform, 1); 
                break;
            case 1:
                SoundFXManager.instance.PlaySoundClip(attack2, transform, 1); 
                break;
            case 2:
                SoundFXManager.instance.PlaySoundClip(attack3, transform, 1); 
                break;
            default:
                break;
        }
    }
}
