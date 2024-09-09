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

    // Update is called once per frame
    void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
        
    }
    public void EquipGun(GameObject newGun)
    {
        if (gun != null)
        {
            // Optionally, destroy the current gun or disable it
            Destroy(gun);
        }

        // Equip the new gun by instantiating it at the gun's position
        gun = Instantiate(newGun, transform.position, Quaternion.identity, transform);
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

    private void HandleGunRotation(){
        //Rotate the gun towards mouse position
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        //Flip the gun when it reaches 90 degree
        angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        Vector3 localscale = new Vector3(1f,1f,1f);
        if (angle > 90|| angle < -90){
            localscale.y = -1f;
        }
        else
        {
            localscale.y = 1f;
        } 
        }

    private void HandleGunShooting(){
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //spawn our bullet
            bulletIns = Instantiate(bullet , bulletSpawnPoint.position , gun.transform.rotation);

            
        }
    }
 
    
}