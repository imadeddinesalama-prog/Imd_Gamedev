using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce = 10f;

    [Header("Jump")]
    public float jumpForce = 5f; // used as jump velocity
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.15f;
    public float groundCheckOffset = 0.05f; // how far above the collider bottom to place the check

    private Rigidbody rb;
    private Collider col;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        CheckGround();

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        rb.AddForce(movement * moveForce);
    }

    void CheckGround()
    {
        Vector3 origin = transform.position;

        if (col != null)
        {
            origin = transform.position - Vector3.up * (col.bounds.extents.y - groundCheckOffset);
        }
        else
        {
            origin = transform.position - Vector3.up * groundCheckOffset;
        }

        isGrounded = Physics.CheckSphere(origin, groundCheckRadius, groundLayer, QueryTriggerInteraction.Ignore);
    }

    void Jump()
    {
        // Set vertical velocity directly for a consistent jump regardless of mass
        Vector3 v = rb.linearVelocity;
        v.y = jumpForce;
        rb.linearVelocity = v;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the ground check sphere
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position;
        if (col != null)
        {
            origin = transform.position - Vector3.up * (col.bounds.extents.y - groundCheckOffset);
        }
        else
        {
            origin = transform.position - Vector3.up * groundCheckOffset;
        }
        Gizmos.DrawWireSphere(origin, groundCheckRadius);
    }
}
