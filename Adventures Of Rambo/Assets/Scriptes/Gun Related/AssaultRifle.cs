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

    private void Awake()
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

        // Ensure the AudioSource is enabled
        if (audioSource != null && fireSound != null)
        {
            if (!audioSource.enabled)
            {
                audioSource.enabled = true; // Enable the AudioSource
            }
            audioSource.PlayOneShot(fireSound);
        }
        else if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }
        else if (fireSound == null)
        {
            Debug.LogError("Fire sound clip is not assigned!");
        }
    }
    else
    {
        Debug.Log("Out of Ammo");
        if (playerAimAndShoot != null)
        {
            playerAimAndShoot.DropGun();
            Debug.Log("Gun dropped.");
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
