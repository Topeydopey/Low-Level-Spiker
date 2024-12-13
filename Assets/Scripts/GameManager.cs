using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] pathmakers; // Array of active Pathmakers
    public GameObject[] tiles;      // Array of generated tiles
    public GameObject pathmakerPrefab; // Reference to the Pathmaker prefab
    public Transform startingPoint; // The initial position for the first Pathmaker

    public void RegenerateMap()
    {
        // Destroy all current Pathmakers
        pathmakers = GameObject.FindGameObjectsWithTag("Pathmaker");
        foreach (GameObject pathmaker in pathmakers)
        {
            Destroy(pathmaker);
        }

        // Destroy all generated tiles
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Destroy(tile);
        }

        // Reset global counters
        Pathmaker.globalTileCount = 0;

        // Spawn a new Pathmaker at the starting position
        Instantiate(pathmakerPrefab, startingPoint.position, Quaternion.identity);
    }
}
