using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
    private int counter = 0;
    public Transform floorPrefab;
    public Transform pathmakerSquarePrefab;

    // "static" means a variable is shared across all instances
    public static int globalTileCount = 0; // Tracks global tile count
    public static int maxTiles = 500; // Maximum global tile count
    void Update()
    {
        if (counter < 50 && globalTileCount < maxTiles)  // Counter that tracks the pathmaker tile spawn count
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
                Instantiate(pathmakerSquarePrefab, transform.position, Quaternion.identity); // Spawn pathmaker at current location
            }
            Instantiate(floorPrefab, transform.position, Quaternion.identity); // Spawn a floor tile
            transform.Translate(Vector2.up); // Move up X units (in 2D)
            counter++; // add to the counter
            globalTileCount++;
        }
        else
        {
            Destroy(gameObject); // Self-destruct when done with else ifs
        }
    }
}
