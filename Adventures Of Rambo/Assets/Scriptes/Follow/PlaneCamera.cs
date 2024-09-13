using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCamera : MonoBehaviour
{
    public Camera mainCamera; // Drag your main camera here in the Inspector

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            // Set the plane's position to the camera's position
            transform.position = mainCamera.transform.position;

            // Optionally, if you want the plane to match the camera's rotation as well
            transform.rotation = mainCamera.transform.rotation;
        }
    }
}
