using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 50; // Maximum health for the enemy
    private int currentHealth; // Current health for the enemy

    public event Action OnHealthChanged; // Event to notify health changes

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health to the maximum at start
    }

    // Implementation of Damage() method from IDamagable interface
    public void Damage(float damageAmount)
    {
        TakeDamage((int)damageAmount);
        Debug.Log("damage happening");
    }

    // Method to apply damage to the enemy
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(); // Notify that health has changed

        if (currentHealth <= 0)
        {
            Die(); // Call Die() when health reaches zero
        }
    }

    // Method to handle enemy death
    private void Die()
    {
        // Handle enemy death here, e.g., play death animation, drop loot, etc.
        Destroy(gameObject); // Destroy the enemy GameObject upon death
    }

    // Optional method to reset enemy health (e.g., for respawning)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke();
    }

    // Optionally, you can add more logic for interaction (e.g., if the enemy collides with a player or hazard)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Add collision logic if needed, such as dealing damage to the player on collision
        if (collision.gameObject.CompareTag("Player"))
        {
            // Example: Damage the player when the enemy collides with them
            collision.gameObject.GetComponent<IDamagable>()?.Damage(10);
        }
    }
}
