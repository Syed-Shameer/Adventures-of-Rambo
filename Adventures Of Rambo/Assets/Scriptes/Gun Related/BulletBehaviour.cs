using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private float destroytime = 3f;
    [SerializeField] private LayerMask whatDestroysBullet;

    [Header("Normal Bullet Stats")]
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float normalBulletDamage = 1f;

    [Header("Physics Bullet Stats")]
    [SerializeField] private float physicsBulletSpeed = 17.5f;
    [SerializeField] private float physicsBulletGravity = 3f;
    [SerializeField] private float physicsBulletDamage = 2f;

    private Rigidbody2D rb;
    private float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        InitializedBulletStats();
        SetRBStats();
    }

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType;

    private void InitializedBulletStats()
    {
        if (bulletType == BulletType.Normal)
        {
            SetStraightVelocity();
            damage = normalBulletDamage;
        }
        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage = physicsBulletDamage;
        }
    }

   private void OnTriggerEnter2D(Collider2D collision)
{
    // Check if the collision is with a layer that should destroy the bullet
    if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
    {


        // Check if the collided object implements IDamagable
        IDamagable iDamagable = collision.gameObject.GetComponent<IDamagable>();
        if (iDamagable != null)
        {
            // Apply damage to the object
            iDamagable.Damage(damage);
        }

        // Destroy the bullet
        Destroy(gameObject);
    }
}


    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }

    private void SetPhysicsVelocity()
    {
        rb.velocity = transform.right * physicsBulletSpeed;
    }

    private void SetRBStats()
    {
        if (bulletType == BulletType.Normal)
        {
            rb.gravityScale = 0;
        }
        else if (bulletType == BulletType.Physics)
        {
            rb.gravityScale = physicsBulletGravity;
        }
    }

    private void FixedUpdate()
    {
        if (bulletType == BulletType.Physics)
        {
            // Rotate bullet in the direction of velocity
            transform.right = rb.velocity;
        }
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroytime);
    }

    // Method to get the bullet's damage value
    public float GetDamage()
    {
        return damage;
    }

    public float GetBulletSpeed()
    {
        if (bulletType == BulletType.Normal)
        {
            return normalBulletSpeed;
        }
        else
        {
            return physicsBulletSpeed;
        }
    }
}
