using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 5f;       // Speed for forward and reverse movement
    public float rotationSpeed = 100f; // Rotation speed for turning
    public Rigidbody2D body;       // Rigidbody for physics-based movement

    void Update()
    {
        // Handle movement input
        float verticalInput = Input.GetAxis("Vertical"); // Forward/Reverse (W/S)
        float horizontalInput = Input.GetAxis("Horizontal"); // Turning (A/D)

        // Move the tank forward or backward
        body.velocity = transform.up * verticalInput * speed;

        // Rotate the tank based on horizontal input
        if (horizontalInput != 0)
        {
            float rotationAmount = -horizontalInput * rotationSpeed * Time.deltaTime;
            body.rotation += rotationAmount;
        }
    }
}
