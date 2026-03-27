using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 14f;

    [Header("Jump Feel")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Angle")]
    public float maxGroundAngle = 46f;

    [Header("Audio")]
    public AudioClip jumpSound;
    [Range(0f, 1f)] public float jumpVolume = 1f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private AudioSource audioSource;
    private bool isDead = false;
    private int groundContactCount = 0;
    public bool isGrounded => groundContactCount > 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            if (jumpSound != null)
            {
                if (audioSource != null)
                    audioSource.PlayOneShot(jumpSound, jumpVolume);
                else
                    AudioSource.PlayClipAtPoint(jumpSound, transform.position, jumpVolume);
            }
        }

        if (rb.linearVelocity.y < 0)
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y
                                 * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y
                                 * (lowJumpMultiplier - 1) * Time.deltaTime;

        anim.SetFloat("Speed", Mathf.Abs(moveInput));
        anim.SetBool("isJumping", !isGrounded);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            UpdateGroundCount();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
            UpdateGroundCount();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Ground"))
        {
            groundContactCount = 0;
            Invoke(nameof(UpdateGroundCount), 0.05f);
        }
    }

    void UpdateGroundCount()
    {
        groundContactCount = 0;
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int count = rb.GetContacts(contacts);

        for (int i = 0; i < count; i++)
        {
            float angle = Vector2.Angle(contacts[i].normal, Vector2.up);
            if (angle < maxGroundAngle)
                groundContactCount++;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Deadly"))
            GetComponent<DeathHandler>().Die();
    }

    public void SetDead(bool value)
    {
        isDead = value;

        if (isDead)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            if (anim != null)
            {
                anim.SetFloat("Speed", 0f);
                anim.SetBool("isJumping", false);
            }
        }
    }
}