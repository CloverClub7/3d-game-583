using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Rigidbody rb;
    private EnemyHealth enemyHealth;
    private float lastAttackTime;

    void Start()
    {
        // Get Rigidbody
        rb = GetComponent<Rigidbody>();

        // Get the EnemyHealth component
        enemyHealth = GetComponent<EnemyHealth>();

        // Find the player if not assigned
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player not found! Make sure player has 'Player' tag.");
            }
        }
    }

    void Update()
    {
        // Check if player and enemyHealth exist
        if (playerTransform == null)
            return;

        if (enemyHealth == null)
            return;

        if (enemyHealth.health <= 0)
            return;

        // Calculate direction to player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0; // Keep movement on horizontal plane

        // Move toward player
        Vector3 movement = direction * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // Rotate to face player
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        // Check if close enough to attack
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PlayerMovement playerMovement = playerTransform.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.takeDamage(damageAmount);
                Debug.Log("Zombie attacked player!");
            }

            lastAttackTime = Time.time;
        }
    }
}