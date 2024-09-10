using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject gunPrefab; // The weapon prefab to be picked up

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the weapon
        if (collision.CompareTag("Player"))
        {
            // Find the player's gun slot
            PlayerAimAndShoot playerAimAndShoot = collision.GetComponent<PlayerAimAndShoot>();

            if (playerAimAndShoot != null)
            {
                // Equip the weapon by passing the gun prefab
                playerAimAndShoot.EquipGun(gunPrefab);
                
                // Optionally, destroy the weapon pickup object
                Destroy(gameObject);
            }
        }
    }
}
