using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
    private int counter = 0;
    private int maxLifetime; // The lifetime for pathmaker
    public Transform[] floorPrefabs; //Array for types of floor
    //public Transform floorPrefab;
    public Transform pathmakerPrefab;
    public LayerMask tileLayerMask; // Filter to check objects only on specific layers

    // "static" means a variable is shared across all instances
    public static int globalTileCount = 0; // Tracks global tile count
    public static int maxTiles = 1500; // Maximum global tile count

    void PlaceTile()
    {
        int randomIndex = Random.Range(0, floorPrefabs.Length); // Random.Range will generates a valid index from 0 to the (array - 1)
        Instantiate(floorPrefabs[randomIndex], transform.position, Quaternion.identity);
    }

    void Start()
    {
        maxLifetime = Random.Range(1000, 2000); // path maker spawns min / max amount of tile before destroying itself (maximum tile limit still applies)
    }
    void Update()
    {
        if (counter < maxLifetime && globalTileCount < maxTiles)  // Counter that tracks the pathmaker tile spawn count
        // Explaination: if counter is less than 50 AND globaltile is less than maxtile then execute if command
        {
            float randomNumber = Random.Range(0f, 1f);
            if (randomNumber < 0.25f)
            {
                transform.Rotate(0, 0, 90f); // Rotate Right
            }
            else if (randomNumber < 0.5f)
            {
                transform.Rotate(0, 0, -90f); // Rotate Left
            }
            else if (randomNumber < 0.99f)
            {
                Instantiate(pathmakerPrefab, transform.position, Quaternion.identity); // Spawn pathmaker at current location
            }
            if (!Physics2D.OverlapCircle(transform.position, 0.1f, tileLayerMask))
            {
                //Instantiate(floorPrefab, transform.position, Quaternion.identity); // Spawn a floor tile}
                PlaceTile();
                globalTileCount++;
            }
            transform.Translate(Vector2.up); // Move up X units (in 2D)
            counter++; // add to the counter
        }
        else
        {
            Destroy(gameObject); // Self-destruct when done with else ifs
        }
    }
}
