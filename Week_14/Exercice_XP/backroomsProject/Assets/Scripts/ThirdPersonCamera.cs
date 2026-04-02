using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;          // Drag Peasant Girl here

    [Header("Camera Settings")]
    public float distance = 5f;       // Distance behind character
    public float height = 2f;         // Height above character
    public float smoothSpeed = 10f;   // How smoothly camera follows

    [Header("Rotation")]
    public float mouseSensitivity = 3f;
    public float minVerticalAngle = -20f;
    public float maxVerticalAngle = 60f;

    private float yaw = 0f;    // Horizontal rotation
    private float pitch = 15f; // Vertical rotation

    void LateUpdate()
    {
        if (target == null) return;

        // Mouse input for rotating camera
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);

        // Calculate desired position
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position
                                + rotation * new Vector3(0, height, -distance);

        // Smoothly move camera there
        transform.position = Vector3.Lerp(transform.position,
                                          desiredPosition,
                                          smoothSpeed * Time.deltaTime);

        // Always look at the character
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }
}