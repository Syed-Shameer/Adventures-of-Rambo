using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
     private GameObject gun;
    [SerializeField] private Transform bulletSpawnPoint;
    private IGuns currentGun;
    [SerializeField] private Transform dropPoint; // Point where the weapon will be dropped
    [SerializeField] private Transform gunAttachmentPoint;
    private bool facingRight = true;

    // Update is called once per frame
    void Update()
    { // Only handle gun actions when a gun is equipped
    if (gun != null)
    {
        HandleGunRotation();
        HandleGunShooting();
    }
    }
    public void EquipGun(GameObject newGun)
    {
        if (gun != null)
        {
            Destroy(gun);
        }

        // Equip the new gun by instantiating it at the gun's position
        gun = Instantiate(newGun, gunAttachmentPoint.position, Quaternion.identity, transform);

        // Pass the PlayerAimAndShoot reference to the newly equipped gun
        IGuns gunScript = gun.GetComponent<IGuns>();
        if (gunScript != null)
        {
            gunScript.SetPlayerAimAndShoot(this);
        }

        // Set the current gun script (which implements IGuns)
        currentGun = gun.GetComponent<IGuns>();

        // Find the BulletSpawnPoint in the newly equipped gun
        bulletSpawnPoint = gun.transform.Find("BulletSpawnPoint");

        if (bulletSpawnPoint == null)
        {
            Debug.LogError("BulletSpawnPoint not found in the new gun.");
        }
    }

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

            Destroy(gun, 3f);

            // Remove the reference to the gun (player is no longer holding it)
            gun = null;
            currentGun = null;
        }
    }

    private void HandleGunRotation()
    {
        // Rotate the gun towards the mouse position
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (worldPosition - (Vector2)gun.transform.position).normalized;

        // Calculate the angle for the gun's rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation to the gun
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Flip the gun based on the player's facing direction
        Vector3 localscale = gun.transform.localScale;
        if (!facingRight)
        {
            localscale.x = -Mathf.Abs(localscale.x); // Ensure the scale is negative to flip
        }
        else
        {
            localscale.x = Mathf.Abs(localscale.x); // Ensure the scale is positive
        }
        gun.transform.localScale = localscale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && currentGun != null)
        {
            currentGun.Fire(); // Call the Fire method from the current gun
        }
    }
}
