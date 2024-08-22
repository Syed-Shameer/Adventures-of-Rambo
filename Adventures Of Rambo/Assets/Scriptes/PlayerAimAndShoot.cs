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
