using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab; // Prefab of the particle system (explosion)
    [SerializeField] private float explosionDuration = 2f; // Time for the explosion to disappear

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit is a bullet or anything that triggers the explosion
        if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Obstacle"))
        {
            // Trigger explosion effect
            Explode();

            // Destroy the bullet or other objects involved in the collision
            Destroy(collision.gameObject);
        }
    }

    private void Explode()
    {
        // Instantiate the explosion particle system at the current position
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Destroy the explosion object after the particle system duration
        Destroy(explosion, explosionDuration);

        // Optionally, destroy the object that exploded
        Destroy(gameObject);
        Debug.Log("Explode");
    }
}
