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

    public void Fire()
    {
        if (ammoCount > 0 && Time.time >= nextFireTime)
        {
            // Spawn a single bullet
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            ammoCount--;
            nextFireTime = Time.time + fireRate;
        }
        else if (ammoCount <= 0)
        {
            Debug.Log("Out of Ammo");
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
