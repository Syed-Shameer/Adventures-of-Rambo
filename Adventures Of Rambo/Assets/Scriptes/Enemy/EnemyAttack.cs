using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject throwablePrefab;     // Throwable prefab reference (e.g., snowball, rock, etc.)
    public Transform throwableSpawnPoint;  // Spawn point for the throwable
    public float attackCooldown = 2f;      // Cooldown time between attacks
    public float detectionRange = 10f;     // Range at which the enemy can attack
    public float throwSpeed = 5f;          // Speed of the throwable object
    private Transform player;              // Reference to the player's transform
    private Animator animator;             // Reference to the Animator component
    private float lastAttackTime = 0f;     // Tracks the time of the last attack

    private void Start()
    {
        // Find the player by tag (make sure your player is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null) return;

        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within attack range
        if (distanceToPlayer < detectionRange)
        {
            // If enough time has passed since the last attack, perform a new attack
            if (Time.time > lastAttackTime + attackCooldown)
            {
                ThrowAtPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    // Method to throw the throwable object (e.g., snowball or rock) at the player
    private void ThrowAtPlayer()
    {
        // Instantiate the throwable object at the spawn point
        GameObject throwable = Instantiate(throwablePrefab, throwableSpawnPoint.position, throwableSpawnPoint.rotation);

        // Calculate the direction towards the player
        Vector2 direction = (player.position - throwableSpawnPoint.position).normalized;

        // Set the velocity of the throwable to move towards the player
        Rigidbody2D rb = throwable.GetComponent<Rigidbody2D>();
        rb.velocity = direction * throwSpeed;

        // Optional: Rotate the throwable towards the player (for projectile visuals)
        throwable.transform.right = direction;

    }
}
