using UnityEngine;

public class HUDFollowCamera : MonoBehaviour
{
    public Transform player; // The player or object to follow
    public RectTransform canvasTransform; // The Canvas RectTransform

    private Vector3 initialPosition; // Initial position of the Canvas

    void Start()
    {
        // Store the initial position of the Canvas
        if (canvasTransform != null)
        {
            initialPosition = canvasTransform.position;
        }
    }

    void Update()
    {
        if (player != null && canvasTransform != null)
        {
            // Update the Canvas position to follow the player's X-axis movement
            Vector3 newPosition = initialPosition;
            newPosition.x = player.position.x;

            // Update the Canvas position
            canvasTransform.position = newPosition;
        }
    }
}
