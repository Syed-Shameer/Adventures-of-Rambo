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
        }
        else
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
