using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private GameObject bulletIns;

    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;
    [SerializeField] private Transform dropPoint; // Point where the weapon will be dropped
    [SerializeField] private Transform gunAttachmentPoint;
     private bool facingRight = true;


    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        
    }
    public void EquipGun(GameObject newGun){
        if (gun != null)
        {
            // Optionally, destroy the current gun or disable it
            Destroy(gun);
        }

        // Equip the new gun by instantiating it at the gun's position
        gun = Instantiate(newGun, gunAttachmentPoint.position, Quaternion.identity, transform);

        // Find the BulletSpawnPoint in the newly equipped gun
        bulletSpawnPoint = gun.transform.Find("BulletSpawnPoint");

        if (bulletSpawnPoint == null)
        {
            Debug.LogError("BulletSpawnPoint not found in the new gun.");
        }
    }

      // Method to drop the current gun
    public void DropGun()
    {
        if (gun != null)
        {
            // Detach the gun from the player and drop it in the world
            gun.transform.SetParent(null);
            gun.transform.position = dropPoint.position; // Set the position to the drop point
            gun.AddComponent<Rigidbody2D>(); // Optionally, add physics for falling
            gun.AddComponent<BoxCollider2D>(); // Add collider to make it interactable
            gun.tag = "Weapon"; // Tag it as a weapon to allow pickup again

            // Remove the reference to the gun (player is no longer holding it)
            gun = null;
        }
    }

   private void HandleGunRotation() {
    // Rotate the gun towards the mouse position
    worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    direction = (worldPosition - (Vector2)gun.transform.position).normalized;

    // Calculate the angle for the gun's rotation
    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Apply rotation to the gun
    gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    // Flip the gun based on the player's facing direction
    Vector3 localscale = gun.transform.localScale;
    if (!facingRight) {
        localscale.x = -Mathf.Abs(localscale.x); // Ensure the scale is negative to flip
    } else {
        localscale.x = Mathf.Abs(localscale.x); // Ensure the scale is positive
    }
    gun.transform.localScale = localscale;
}


 private void HandleGunShooting() {
    if (Mouse.current.leftButton.wasPressedThisFrame) {
        // Spawn the bullet
        bulletIns = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);

        // Set bullet velocity based on the gun's direction
        Rigidbody2D bulletRb = bulletIns.GetComponent<Rigidbody2D>();
        if (bulletRb != null) {
            bulletRb.velocity = gun.transform.right * bulletIns.GetComponent<BulletBehaviour>().GetBulletSpeed();
        }
    }
}

    
}