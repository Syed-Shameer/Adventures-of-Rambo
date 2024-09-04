using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;  // Reference to the player's transform
    public Vector3 offset = new Vector3(0, 0, -10);  // Offset from the player (keep Z constant)
    public float smoothSpeed = 0.125f;  // Speed of camera smoothing
    public float minX;  // Minimum X boundary of the camera
    public float maxX;  // Maximum X boundary of the camera

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Target position based on player's X position and offset
            Vector3 desiredPosition = new Vector3(playerTransform.position.x + offset.x, transform.position.y, offset.z);

            // Clamp the X position within the specified boundaries
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);

            // Smoothly interpolate between the current position and the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the new position to the camera
            transform.position = smoothedPosition;
        }
    }
}
