using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime = 1f; // How long the explosion effect lasts

    void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the explosion after the specified lifetime
    }
}
