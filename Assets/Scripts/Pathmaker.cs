using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
    private int counter = 0;
    private int maxLifetime; // The lifetime for pathmaker
    public Transform[] floorPrefabs; //Array for types of floor
    public Transform[] obstaclePrefabs; // Array for obstacles
    //public static List<Vector2> tilePositions = new List<Vector2>();

    //public Transform floorPrefab;
    //public static List<Vector2> tilePositions = new List<Vector2>();
    public Transform pathmakerPrefab;
    public LayerMask tileLayerMask; // Filter to check objects only on specific layers
    private DynamicCameraScaler cameraScaler;
    // "static" means a variable is shared across all instances
    public static int globalTileCount = 0; // Tracks global tile count
    public static int maxTiles = 1500; // Maximum global tile count
    public int clusterSize = 3; // How many tiles pm place in 1 step
    public int squareSize = 6; // Size of square that pm makes
    public float turnProbability = 0.1f; // How often pm turns
    public float branchProbability = 0.5f; // How often new pm spawns
    public float sa2SpawnProbability = 0.05f;
    //public float specialTileProbability = 0.2f;
    public AudioClip generationSound; // Generation Sound
    public AudioClip finishedSound; // Finished sound
    private AudioSource audioSource;

    void PlaceTile()
    {
        int randomIndex = Random.Range(0, floorPrefabs.Length); // Random.Range will generates a valid index from 0 to the (array - 1)
        Instantiate(floorPrefabs[randomIndex], transform.position, Quaternion.identity);

        if (cameraScaler != null)
        {
            cameraScaler.UpdateBounds(transform.position); // Update position for camera scaling
        }

        if (Random.value < sa2SpawnProbability)
        {
            Debug.Log("Attempting to spawn SAM site.");
            if (!Physics2D.OverlapCircle(transform.position, 0.1f, tileLayerMask) && obstaclePrefabs.Length > 0)
            {
                // Select SA-2 prefab at index 0 in obstaclePrefabs
                Instantiate(obstaclePrefabs[0], transform.position, Quaternion.identity);
            }
        }

        float obstacleChance = 0.3f; // Chance of spawning the obstacle
        if (Random.value < obstacleChance && obstaclePrefabs.Length > 0)
        {
            Debug.Log("Attempts to spawn tile");
            // Check if postion is clear
            if (!Physics2D.OverlapCircle(transform.position, 0.1f, tileLayerMask))
            {
                int randomObstacleIndex = Random.Range(1, obstaclePrefabs.Length); // index 1 to skip SA2 at 0
                Instantiate(obstaclePrefabs[randomObstacleIndex], transform.position, Quaternion.identity); // Spawns obstacles
            }
        }
    }
    void PlaceSquareCluster()
    {
        for (int x = 0; x < squareSize; x++) // Nested X loop that goes horizontally
        {
            for (int y = 0; y < squareSize; y++)
            {
                Vector3 offset = new Vector3(x, y, 0); // Offset for each tile in the square
                Vector3 spawnPosition = transform.position + offset;

                if (!Physics2D.OverlapCircle(spawnPosition, 0.1f, tileLayerMask))
                {
                    int randomIndex = Random.Range(0, floorPrefabs.Length);
                    Instantiate(floorPrefabs[randomIndex], spawnPosition, Quaternion.identity);
                    Pathmaker.globalTileCount++; // Update global tile count
                }
            }
        }
    }
    void Start()
    {
        maxLifetime = Random.Range(2000, 3000); // path maker spawns min / max amount of tile before destroying itself (maximum tile limit still applies)
        cameraScaler = FindObjectOfType<DynamicCameraScaler>();

        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = generationSound;
        audioSource.loop = true; // loops generation sound
        audioSource.playOnAwake = false;
        audioSource.volume = 0.5f;

        if (generationSound != null)
        {
            audioSource.Play();
        }
    }
    void Update()
    {
        if (counter < maxLifetime && globalTileCount < maxTiles)  // Counter that tracks the pathmaker tile spawn count
        // Explaination: if counter is less than 50 AND globaltile is less than maxtile then execute if command
        {
            float randomNumber = Random.Range(0f, 1f);

            if (randomNumber < turnProbability)
            {
                transform.Rotate(0, 0, 90f); // Rotate Right
            }
            else if (randomNumber < turnProbability * 2f)
            {
                transform.Rotate(0, 0, -90f); // Rotate Left
            }
            else if (randomNumber > (1f - branchProbability))
            {
                Instantiate(pathmakerPrefab, transform.position, Quaternion.identity); // Spawn pathmaker at current location
            }
            /*
                        for (int i = 0; i < clusterSize; i++)
                        {
                            if (!Physics2D.OverlapCircle(transform.position, 0.1f, tileLayerMask))
                            {
                                //Instantiate(floorPrefab, transform.position, Quaternion.identity); // Spawn a floor tile}
                                PlaceTile();
                                globalTileCount++;
                            }
                            transform.Translate(Vector2.up); // Move up X units (in 2D)
                        }
             */
            // Spawn a square cluster
            PlaceSquareCluster();
            PlaceTile();
            globalTileCount++;
            // Move forward after placing the cluster
            transform.Translate(Vector2.up * squareSize);
            counter++; // add to the counter
        }
        else
        {
            // When finished, stop the generation sound and play the finished sound
            if (globalTileCount >= maxTiles)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                if (finishedSound != null)
                {
                    audioSource.PlayOneShot(finishedSound);
                }
            }

            Destroy(gameObject);
        }
    }
}
