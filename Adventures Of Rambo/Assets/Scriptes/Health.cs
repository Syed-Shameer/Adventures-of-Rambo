using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health value
    private int currentHealth; // Current health value
    private RagdollController ragdollController;
    private Rigidbody2D mainRigidbody;
    private PlayerMovement playerMovement; // Reference to the input handling script

    // Event to notify health changes
    public event Action OnHealthChanged;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;

    private void Start()
    {
        // Set the current health to the maximum health at the start
        currentHealth = maxHealth;

        // Get the RagdollController and PlayerMovement components attached to this GameObject
        ragdollController = GetComponent<RagdollController>();
        mainRigidbody = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>(); // Adjust this line if your input script has a different name
    }

    // Method to apply damage to the character
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(); // Notify that health has changed

        // Check if the health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle character death
    private void Die()
    {
        // Activate the ragdoll effect
        ragdollController.ActivateRagdoll();

        // Disable player movement/input handling
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Additional logic for death (e.g., play sound, trigger game over, etc.)
    }

    // Condition 1: Player dies when falling badly
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    // Optional method to reset health (e.g., on respawn)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        ragdollController.DeactivateRagdoll();

        // Reactivate player movement/input handling
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        OnHealthChanged?.Invoke(); // Notify that health has changed
    }
}
