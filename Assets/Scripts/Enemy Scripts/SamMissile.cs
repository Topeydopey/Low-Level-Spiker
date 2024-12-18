using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamMissile : MonoBehaviour
{
    public float speed = 5f;         // Missile speed
    public float rotationSpeed = 200f; // How quickly the missile turns towards the target
    public float lifetime = 10f;    // How long the missile exists before self-destructing
    public GameObject explosionEffect; // Prefab for explosion effect

    private Transform target;       // The current target of the missile (e.g., the player)

    void Start()
    {
        // Find the player object by locating the specific component
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            target = player.transform;
        }

        // Destroy the missile after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
            direction.Normalize();

            // Calculate the angle to rotate towards the target
            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            // Apply rotation
            transform.Rotate(0, 0, -rotateAmount * rotationSpeed * Time.deltaTime);
        }

        // Move forward in the missile's current direction
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the missile hits the player by verifying the PlayerMovement component
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Missile hit the player!");

            // Trigger explosion effect
            Explode();
        }

        // Check if the missile hits chaff by verifying the Chaff component
        if (collision.GetComponent<Chaff>() != null)
        {
            Debug.Log("Missile distracted by chaff!");

            // Trigger explosion effect
            Explode();
        }
    }

    private void Explode()
    {
        // Instantiate explosion effect if available
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // Destroy the missile
    }
}
