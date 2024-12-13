using System.Collections.Generic;
using UnityEngine;

public class DynamicCameraScaler : MonoBehaviour
{
    public Camera cameraToScale; // Reference to the camera
    public float padding = 2f;   // Extra space around the level for padding

    private float minX = Mathf.Infinity;
    private float maxX = Mathf.NegativeInfinity;
    private float minY = Mathf.Infinity;
    private float maxY = Mathf.NegativeInfinity;

    void Start()
    {
        // Assign the main camera if not manually assigned
        if (cameraToScale == null)
        {
            cameraToScale = Camera.main;
        }
    }

    public void UpdateBounds(Vector2 newPosition)
    {
        // Update bounds based on the new tile position
        if (newPosition.x < minX) minX = newPosition.x;
        if (newPosition.x > maxX) maxX = newPosition.x;
        if (newPosition.y < minY) minY = newPosition.y;
        if (newPosition.y > maxY) maxY = newPosition.y;

        UpdateCamera();
    }

    void UpdateCamera()
    {
        // Calculate center of the bounds
        float centerX = (minX + maxX) / 2f;
        float centerY = (minY + maxY) / 2f;

        // Calculate the size of the bounds
        float width = maxX - minX;
        float height = maxY - minY;

        // Set the camera position
        cameraToScale.transform.position = new Vector3(centerX, centerY, -10f);

        // Adjust camera size based on the largest dimension
        float largestDimension = Mathf.Max(width, height);
        cameraToScale.orthographicSize = largestDimension / 2f + padding;
    }
}
