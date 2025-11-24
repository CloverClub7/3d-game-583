using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0.5f, 2.5f, -4f); // Slightly right, above, and behind
    public float followSpeed = 10f;
    public float rotationDamp = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Smooth rotation to look at the player
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationDamp * Time.deltaTime);
    }
}
