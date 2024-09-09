using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGun : MonoBehaviour
{
    [SerializeField] protected Transform BulletSpawnPoint;

    public Transform GetBulletSpawnPoint()
    {
        return BulletSpawnPoint;
    }

    public abstract void Fire();

}


