using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;
    public float attackRange = 1.5f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;
    private float attackCooldownTimer;

    private void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange && attackCooldownTimer <= 0)
        {
            AttackPlayer();
            attackCooldownTimer = attackCooldown; // Reset cooldown
        }
    }

    void AttackPlayer()
    {
        // Assume player has a PlayerHealth script
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}

