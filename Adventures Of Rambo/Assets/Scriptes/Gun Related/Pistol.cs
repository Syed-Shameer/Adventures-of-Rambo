using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IGuns
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private int ammoCount = 12;
    private PlayerAimAndShoot playerAimAndShoot; // Reference to the player script

    // Set the reference to the PlayerAimAndShoot script
    public void SetPlayerAimAndShoot(PlayerAimAndShoot player)
    {
        playerAimAndShoot = player;
    }

    public void Fire()
    {
        if (ammoCount > 0)
        {
            // Spawn the bullet
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            ammoCount--;
        }
        else
        {
            Debug.Log("Out of Ammo");

            // Call DropGun from the player's script when out of ammo
            if (playerAimAndShoot != null)
            {
                playerAimAndShoot.DropGun();
            }
        }
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }
}
