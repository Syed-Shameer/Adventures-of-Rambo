using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour
{
    public float speed = 5f;           // Speed of the snowball
    public float damageAmount = 10f;   // Damage dealt to the player
    public float lifetime = 5f;        // Time in seconds before the snowball is destroyed

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Move the snowball forward

        // Start the coroutine to destroy the snowball after a delay
        StartCoroutine(DestroyAfterTime(lifetime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the snowball collides with the player
        if (other.CompareTag("Player"))
        {
            // Apply damage to the player
            other.GetComponent<IDamagable>()?.Damage(damageAmount);

            // Destroy the snowball after hitting the player
            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground") || other.CompareTag("Wall"))
        {
            // Destroy the snowball if it hits something else (ground, wall, etc.)
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
