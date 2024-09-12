using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour, IGuns
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 1.0f; // Slower fire rate
    [SerializeField] private int ammoCount = 6; // Smaller magazine size
    [SerializeField] private int pelletsPerShot = 5; // Number of pellets fired in a spread
    [SerializeField] private AudioClip fireSound; // Reference to the shotgun firing sound
    private AudioSource audioSource; // Reference to the AudioSource component
    private PlayerAimAndShoot playerAimAndShoot; // Reference to the player script

    private void Awake()
    {
        // Get the AudioSource component attached to the gun GameObject
        audioSource = GetComponent<AudioSource>();

        // If AudioSource is missing, add it
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Set the reference to the PlayerAimAndShoot script
    public void SetPlayerAimAndShoot(PlayerAimAndShoot player)
    {
        playerAimAndShoot = player;
    }

   public void Fire()
{
    if (ammoCount > 0)
    {
        for (int i = 0; i < pelletsPerShot; i++)
        {
            // Spread the bullets by adding random variation to the angle
            float spreadAngle = Random.Range(-10f, 10f);
            Quaternion spreadRotation = Quaternion.Euler(0, 0, bulletSpawnPoint.eulerAngles.z + spreadAngle);

            // Spawn each pellet
            Instantiate(bulletPrefab, bulletSpawnPoint.position, spreadRotation);
        }

        ammoCount--;

        // Ensure the AudioSource is enabled before playing the sound
        if (audioSource != null && fireSound != null)
        {
            if (!audioSource.enabled)
            {
                audioSource.enabled = true; // Enable the AudioSource if it's disabled
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
