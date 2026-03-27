using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Settings")]
    public float smoothSpeed = 5f;
    public Vector2 offset = new Vector2(0f, 1f);

    [Header("Lock Axes")]
    public bool lockX = false;
    public bool lockY = false;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
                player = p.transform;
            
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (!lockX) targetX = player.position.x + offset.x;
        if (!lockY) targetY = player.position.y + offset.y;

        Vector3 targetPos = new Vector3(targetX, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}