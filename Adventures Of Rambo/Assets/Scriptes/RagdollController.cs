using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody2D[] rigidbodies;
    private Collider2D[] colliders;
    private Animator animator;

    private void Awake()
    {
        // Get all Rigidbody2D and Collider2D components from the child objects
        rigidbodies = GetComponentsInChildren<Rigidbody2D>();
        colliders = GetComponentsInChildren<Collider2D>();
        animator = GetComponent<Animator>();

        // Initially disable ragdoll behavior
        SetRagdollState(false);
    }

    // Method to enable or disable the ragdoll state
    public void SetRagdollState(bool state)
    {
        foreach (Rigidbody2D rb in rigidbodies)
        {
            rb.isKinematic = !state;
        }

        foreach (Collider2D col in colliders)
        {
            col.enabled = state;
        }

        if (animator != null)
        {
            animator.enabled = !state;
        }
    }

    // Method to activate ragdoll (can be called when character dies or gets hit)
    public void ActivateRagdoll()
    {
        SetRagdollState(true);
    }

    // Method to deactivate ragdoll (can be used when respawning or resetting character)
    public void DeactivateRagdoll()
    {
        SetRagdollState(false);
    }
}
