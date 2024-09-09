using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody2D mainRigidbody;
    private Collider2D mainCollider;
    private Animator animator;

    private List<Rigidbody2D> limbRigidbodies = new List<Rigidbody2D>();
    private List<Collider2D> limbColliders = new List<Collider2D>();
    private List<HingeJoint2D> limbJoints = new List<HingeJoint2D>();

    private void Awake()
    {
        mainRigidbody = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        foreach (Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
        {
            if (rb != mainRigidbody)
            {
                limbRigidbodies.Add(rb);
                rb.isKinematic = true;
            }
        }

        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            if (col != mainCollider)
            {
                limbColliders.Add(col);
                col.enabled = false;
            }
        }

        foreach (HingeJoint2D joint in GetComponentsInChildren<HingeJoint2D>())
        {
            limbJoints.Add(joint);
            joint.enabled = false;
        }
    }

    public void ActivateRagdoll()
    {
        animator.enabled = false;
        mainRigidbody.isKinematic = true;
        mainCollider.enabled = false;

        foreach (Rigidbody2D rb in limbRigidbodies)
        {
            rb.isKinematic = false;
        }

        foreach (Collider2D col in limbColliders)
        {
            col.enabled = true;
        }

        foreach (HingeJoint2D joint in limbJoints)
        {
            joint.enabled = true;
        }
    }

    public void DeactivateRagdoll()
    {
        animator.enabled = true;
        mainRigidbody.isKinematic = false;
        mainCollider.enabled = true;

        foreach (Rigidbody2D rb in limbRigidbodies)
        {
            rb.isKinematic = true;
        }

        foreach (Collider2D col in limbColliders)
        {
            col.enabled = false;
        }

        foreach (HingeJoint2D joint in limbJoints)
        {
            joint.enabled = false;
        }
    }
}

