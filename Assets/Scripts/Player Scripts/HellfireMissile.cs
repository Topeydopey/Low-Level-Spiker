using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireMissile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 10f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SA2"))
        {
            Destroy(other.gameObject); // Destroy enemy
            Destroy(gameObject); // Destroy self
        }
    }
}
