using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Maximum health value
    private int currentHealth; // Current health value
    private RagdollController ragdollController;

    private void Start()
    {
        // Set the current health to the maximum health at the start
        currentHealth = maxHealth;

        // Get the RagdollController component attached to this GameObject
        ragdollController = GetComponent<RagdollController>();
    }

    // Method to apply damage to the character
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to heal the character (optional)
    public void Heal(int amount)
    {
        currentHealth += amount;
        // Ensure health doesn't exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Method to handle character death
    private void Die()
    {
        // Activate the ragdoll effect
        ragdollController.ActivateRagdoll();

        // Additional logic for death (e.g., disable movement, play sound, etc.)
        // For example:
        // GetComponent<PlayerMovement>().enabled = false;
        // Play death sound, trigger game over, etc.
    }

    // Optional method to reset health (e.g., on respawn)
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        ragdollController.DeactivateRagdoll();
        // Reactivate movement or other scripts if necessary
    }
}
