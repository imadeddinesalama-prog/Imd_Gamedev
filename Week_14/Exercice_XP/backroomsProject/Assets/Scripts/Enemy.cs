using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public int xpReward = 50; 

    [Header("Visual Feedback")]
    public GameObject deathEffect; 
    public float destroyDelay = 2f; 

    [Header("Audio")]
    public AudioClip hitSound;
    public AudioClip deathSound;

    private Animator animator;
    private AudioSource audioSource;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        Debug.Log($"{gameObject.name} took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log($"{gameObject.name} defeated!");

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            XPManager xpManager = player.GetComponent<XPManager>();
            if (xpManager != null)
            {
                xpManager.AddXP(xpReward);
                Debug.Log($"Player gained {xpReward} XP!");
            }
        }

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        DisableEnemy();

        Destroy(gameObject, destroyDelay);
    }

    void DisableEnemy()
    {
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

    }

    void OnGUI()
    {
        if (isDead) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
        if (screenPos.z > 0)
        {
            GUI.Label(new Rect(screenPos.x - 50, Screen.height - screenPos.y - 20, 100, 20),
                     $"HP: {currentHealth}/{maxHealth}");
        }
    }
}