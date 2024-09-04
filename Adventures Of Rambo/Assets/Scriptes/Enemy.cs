using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 5f;
    private float currentHealth;
    public int scoreValueOnDamage = 1;
    public int scoreValueOnDeath = 10;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Add score for damaging the enemy
        ScoreManager.instance.AddScore(scoreValueOnDamage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Add score for killing the enemy
        ScoreManager.instance.AddScore(scoreValueOnDeath);

        // Add additional logic here (e.g., activate ragdoll, destroy enemy, etc.)
        Destroy(gameObject);  // Destroy the enemy GameObject
    }
}
