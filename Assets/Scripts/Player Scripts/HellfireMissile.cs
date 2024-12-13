using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireMissile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the missile after its lifetime expires
    }

    void Update()
    {
        // Move the missile forward in the direction it is facing
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the missile hits an SA-2 by verifying the SA2 component
        if (other.GetComponent<Sa2Site>() != null)
        {
            Debug.Log("Hellfire hit the SA-2 site!");
            Destroy(other.gameObject); // Destroy the SA-2
            Destroy(gameObject);       // Destroy the missile
        }
    }
}
