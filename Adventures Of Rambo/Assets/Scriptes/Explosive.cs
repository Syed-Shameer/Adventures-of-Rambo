using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float health = 100f; // Barrel health before it explodes
    [SerializeField] private GameObject explosionEffect; // Reference to particle system prefab for explosion
    [SerializeField] private float explosionRadius = 5f; // Radius of the explosion
    [SerializeField] private int explosionDamage = 40; // Damage dealt to nearby objects
    [SerializeField] private float explosionDuration = 2f; // Time before the barrel is destroyed after explosion
    [SerializeField] private AudioClip explosionSound; // Sound effect for the explosion
    private AudioSource audioSource; // Reference to AudioSource component

    private bool hasExploded = false;

    private void Awake()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Method to take damage when hit
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !hasExploded)
        {
            Explode();
        }
    }

    // Handle triggers with bullets and other objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected with: " + collision.gameObject.name);

        if (collision.CompareTag("bullet"))
        {
            Debug.Log("Trigger with bullet detected.");

            BulletBehaviour bullet = collision.GetComponent<BulletBehaviour>();
            if (bullet != null)
            {
                TakeDamage(bullet.GetDamage());
                Destroy(collision.gameObject); // Destroy the bullet on impact
            }
        }
    }

    // Explosion logic
    private void Explode()
    {
        hasExploded = true;

        // Play the explosion sound
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Instantiate explosion effect and play it
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play(); // Manually trigger the particle system to play
        }

        // Deal damage to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            // Apply damage to objects with health components (e.g., enemies, player, etc.)
            Health target = nearbyObject.GetComponent<Health>();
            if (target != null)
            {
                target.TakeDamage(explosionDamage);
            }
        }

        // Destroy the barrel and explosion effect after the duration
        Destroy(explosion, explosionDuration); // Destroy explosion effect after it finishes
        Destroy(gameObject, explosionDuration); // Destroy barrel after the explosion effect
    }

    // To visualize explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
