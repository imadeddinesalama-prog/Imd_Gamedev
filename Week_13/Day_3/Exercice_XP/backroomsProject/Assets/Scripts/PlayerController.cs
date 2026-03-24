using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public float jumpForce = 5f;

    [Header("Footsteps")]
    public AudioClip[] footstepSounds;
    public float walkStepInterval = 0.4f;
    public float runStepInterval = 0.25f; // faster steps when running

    private Animator animator;
    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isGrounded = true;
    private float footstepTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationZ;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Right click to run
        bool isRunning = Input.GetMouseButton(1);

        // Get camera direction (ignore Y)
        Camera cam = Camera.main;
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = (camForward * v + camRight * h);
        float speed = direction.magnitude;

        // Only run if actually moving
        bool actuallyRunning = isRunning && speed > 0.1f;

        animator.SetFloat("Speed", speed);
        animator.SetBool("isRunning", actuallyRunning);

        float currentSpeed = actuallyRunning ? runSpeed : moveSpeed;

        if (speed > 0.1f)
        {
            direction.Normalize();

            // Move
            transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);

            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                   targetRotation,
                                                   rotationSpeed * Time.deltaTime);
        }

        HandleFootsteps(speed, actuallyRunning);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            audioSource.Stop();
            footstepTimer = 0f;
        }
    }

    void HandleFootsteps(float speed, bool isRunning)
    {
        if (speed > 0.1f && isGrounded)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
                audioSource.PlayOneShot(clip);

                // Faster footstep rate when running
                footstepTimer = isRunning ? runStepInterval : walkStepInterval;
            }
        }
        else
        {
            audioSource.Stop();
            footstepTimer = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}