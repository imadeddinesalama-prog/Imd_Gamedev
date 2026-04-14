using UnityEngine;

public class PunchHitbox : MonoBehaviour
{
    [Header("Damage Settings")]
    public int punchDamage = 25;

    [Header("Hitbox Control")]
    public float hitboxActiveTime = 0.3f; 

    private bool isActive = false;

    void Start()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void ActivateHitbox()
    {
        isActive = true;
        GetComponent<Collider>().enabled = true;

        Invoke("DeactivateHitbox", hitboxActiveTime);
    }

    public void DeactivateHitbox()
    {
        isActive = false;
        GetComponent<Collider>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(punchDamage);
                Debug.Log($"Punched {other.name} for {punchDamage} damage!");
            }
        }
    }
}