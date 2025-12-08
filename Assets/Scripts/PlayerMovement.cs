using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float sprintSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaDrainRate = 1f;   // per second while sprinting
    public float staminaRegenRate = 2f;   // per second while not sprinting
    public Slider staminaBar;             // UI Slider in canvas

    [Header("Player")]
    public int playerHealth = 10;

    private CharacterController controller;
    private float stamina;
    private float currentSpeed;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stamina = maxStamina;
        currentSpeed = walkSpeed;

        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = maxStamina;
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
        {
            // small negative value keeps controller stuck to the ground
            velocity.y = -2f;
        }

        // --- Movement input ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        bool isMoving = move.magnitude > 0.1f;

        // --- Sprinting + stamina logic ---
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && isMoving;
        bool hasStamina = stamina > 0f;

        if (wantsToSprint && hasStamina)
        {
            currentSpeed = sprintSpeed;
            stamina -= staminaDrainRate * Time.deltaTime;
            if (stamina < 0f) stamina = 0f;
        }
        else
        {
            currentSpeed = walkSpeed;

            // Only regen when not holding sprint
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                stamina += staminaRegenRate * Time.deltaTime;
                if (stamina > maxStamina) stamina = maxStamina;
            }
        }

        // Apply horizontal movement
        controller.Move(move * currentSpeed * Time.deltaTime);

        // --- Jumping ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // --- Gravity ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Update stamina UI ---
        if (staminaBar != null)
        {
            staminaBar.value = stamina;
        }
    }

    public void takeDamage(int amount)
    {
        playerHealth -= amount;
        Debug.Log("Player has taken damage! New HP: " + playerHealth);
    }
}
