using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IGuns
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.1f; // Fast firing rate
    [SerializeField] private int ammoCount = 30;   // Larger ammo count
    private float nextFireTime = 0f;
    private PlayerAimAndShoot playerAimAndShoot; // Reference to the player script
    [SerializeField] private AudioClip fireSound; // Reference to the gunshot sound effect
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component attached to the gun GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Set the reference to the PlayerAimAndShoot script
    public void SetPlayerAimAndShoot(PlayerAimAndShoot player)
    {
        playerAimAndShoot = player;
    }

    public void Fire()
    {
        if (ammoCount > 0 && Time.time >= nextFireTime)
        {
            // Spawn a single bullet
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            ammoCount--;
            nextFireTime = Time.time + fireRate;

            // Play the firing sound
            if (audioSource != null && fireSound != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }
        else 
        {
            Debug.Log("Out of Ammo");
            // Call DropGun from the player's script when out of ammo
            if (playerAimAndShoot != null)
            {
                playerAimAndShoot.DropGun();
                Debug.Log("DropGun");
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
