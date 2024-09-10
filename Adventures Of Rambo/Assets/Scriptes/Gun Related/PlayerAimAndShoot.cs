using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    private IGuns currentGun;
    private GameObject gunObject;
    [SerializeField] private Transform gunAttachmentPoint;
    [SerializeField] private Transform dropPoint;
    private bool facingRight = true;

    void Update()
    {
        HandleGunRotation();
        if (currentGun != null) 
        {
            HandleGunShooting();
        }
    }

    public void EquipGun(GameObject newGunPrefab)
    {
        // Destroy current gun if any
        if (gunObject != null)
        {
            Destroy(gunObject);
        }

        // Instantiate and equip the new gun
        gunObject = Instantiate(newGunPrefab, gunAttachmentPoint.position, Quaternion.identity, transform);
        gunObject.transform.SetParent(gunAttachmentPoint);

        // Get the IGuns interface from the new gun
        currentGun = gunObject.GetComponent<IGuns>();
        
        if (currentGun == null)
        {
            Debug.LogError("Equipped object does not implement IGuns interface!");
        }
    }

    private void HandleGunRotation()
    {
        // Rotate the gun towards the mouse position
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (worldPosition - (Vector2)gunObject.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gunObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Flip gun scale for facing direction
        Vector3 localscale = gunObject.transform.localScale;
        localscale.x = facingRight ? Mathf.Abs(localscale.x) : -Mathf.Abs(localscale.x);
        gunObject.transform.localScale = localscale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            currentGun.Fire();
        }
    }

    public void DropGun()
    {
        if (gunObject != null)
        {
            gunObject.transform.SetParent(null);
            gunObject.transform.position = dropPoint.position; // Set the position to the drop point
            gunObject.AddComponent<Rigidbody2D>(); // Add physics for falling
            gunObject.AddComponent<BoxCollider2D>(); // Add collider to make it interactable
            gunObject.tag = "Weapon"; // Tag it as a weapon to allow pickup again
            gunObject = null;
            currentGun = null; // Remove reference to the gun
        }
    }
}
