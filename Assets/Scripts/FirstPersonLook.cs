using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Vertical look (clamped)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate camera up/down
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player body left/right
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
