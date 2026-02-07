using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection & Attack")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private string playerTag = "Player";

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    private Transform player;
    private float lastAttackTime;

    void Update()
    {
        // Find player if not found yet
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return;
        }

        // Check distance to player
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Player is very close - attack
            AttackPlayer();
        }
        else if (distance <= chaseRange)
        {
            // Player is close - chase them
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Move toward player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep enemy on ground level

        transform.position += direction * moveSpeed * Time.deltaTime;

        // Look at player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void AttackPlayer()
    {
        // Look at player while attacking
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log("Enemy attacked player for " + attackDamage + " damage!");
            }

            lastAttackTime = Time.time;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize chase range (yellow)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Visualize attack range (red)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}