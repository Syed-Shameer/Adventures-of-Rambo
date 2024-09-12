using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IGuns
{
    [SerializeField] private GameObject pelletPrefab; // Prefab for shotgun pellets
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private int pelletCount = 8; // Number of pellets per shot
    [SerializeField] private float spreadAngle = 10f; // Spread angle between pellets
    [SerializeField] private float fireRate = 0.8f; // Slower fire rate for a shotgun
    [SerializeField] private int ammoCount = 6;   // Typical shotgun ammo count
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
            // Fire multiple pellets with spread
            for (int i = 0; i < pelletCount; i++)
            {
                // Calculate the spread by rotating the bullet spawn point
                float randomSpread = Random.Range(-spreadAngle / 2, spreadAngle / 2);
                Quaternion spreadRotation = Quaternion.Euler(0, 0, randomSpread);
                
                // Instantiate each pellet with the calculated spread
                Instantiate(pelletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation * spreadRotation);
            }

            ammoCount--;
            nextFireTime = Time.time + fireRate;

            // Ensure the AudioSource is enabled
            if (audioSource != null && fireSound != null)
            {
                if (!audioSource.enabled)
                {
                    audioSource.enabled = true; // Enable the AudioSource
                }
                audioSource.PlayOneShot(fireSound); // Play the firing sound
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

            // Call DropGun from the player's script when out of ammo
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
