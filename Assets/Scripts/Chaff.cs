using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaff : MonoBehaviour
{
    public float lifetime = 4f; // How long the explosion effect lasts

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the explosion after the specified lifetime
    }
}
