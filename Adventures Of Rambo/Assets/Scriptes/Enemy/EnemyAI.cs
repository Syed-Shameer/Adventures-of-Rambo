using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;  // Distance at which the enemy starts chasing the player
    public float chaseSpeed = 2f;       // Speed of the enemy when chasing the player
    public float idleSpeed = 0f;        // Speed when enemy is idle
    private Transform player;           // Reference to the player's transform
    private Animator animator;          // Reference to the Animator component
    private bool isChasing = false;     // Tracks if the enemy is currently chasing the player
    private Rigidbody2D enemyRigidbody; // Reference to the Rigidbody2D component

    private void Start()
    {
        // Find the player by tag (make sure your player GameObject is tagged as "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the Animator component from the enemy
        animator = GetComponent<Animator>();

        // Get the Rigidbody2D component for movement
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Calculate distance to the player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Determine whether to chase or stay idle based on distance
        if (distanceToPlayer < detectionRange)
        {
            // Chase the player
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            // Enemy stays idle
            isChasing = false;
            Idle();
        }

        // Set the isRunning parameter in the Animator
        animator.SetBool("isRunning", isChasing);
    }

    // Method to chase the player
    private void ChasePlayer()
    {
        // Calculate the direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move towards the player using the rigidbody
        enemyRigidbody.velocity = direction * chaseSpeed;

        // Flip enemy based on player position (optional)
        FlipTowardsPlayer(direction);
    }

    // Method to make the enemy idle
    private void Idle()
    {
        // Set velocity to 0 to stop any movement
        enemyRigidbody.velocity = Vector2.zero;
    }

    // Method to flip enemy to face the player (optional, useful for 2D side-scrollers)
    private void FlipTowardsPlayer(Vector2 direction)
    {
        if (direction.x > 0 && transform.localScale.x < 0)
        {
            // Face right
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (direction.x < 0 && transform.localScale.x > 0)
        {
            // Face left
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
