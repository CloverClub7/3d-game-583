using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float regenRate = 5f;
    public float regenDelay = 3f;

    private float currentHealth;
    private float lastHitTime;
    private bool isDead = false;

    [Header("UI")]
    public Slider healthBar;           // Assign your health slider
    public GameObject deathScreenUI;   // Assign the DeathScreen panel

    void Start()
    {
        currentHealth = maxHealth;

        // Set up UI
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }

        // Hide the death screen at the start
        if (deathScreenUI != null)
            deathScreenUI.SetActive(false);
    }

    void Update()
    {
        if (isDead)
            return;

        // Regen health after delay
        if (Time.time - lastHitTime > regenDelay && currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }

        // Update UI
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        lastHitTime = Time.time;

        Debug.Log($"Player has taken damage! New HP: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        Debug.Log("Player Died!");

        // Show the death UI
        if (deathScreenUI != null)
            deathScreenUI.SetActive(true);

        // Freeze game
        Time.timeScale = 0f;

        // 🔓 Unlock and show the mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Connected to Restart Button
    public void RestartGame()
    {
        // Resume time
        Time.timeScale = 1f;

        // 🔒 Lock and hide the mouse cursor again for FPS control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
