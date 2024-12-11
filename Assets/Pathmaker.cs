using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathmaker : MonoBehaviour
{
    private int counter = 0;
    public Transform floorPrefab;
    public Transform pathmakerSquarePrefab;

    void Update()
    {
        if (counter < 50)
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
            transform.Translate(Vector2.up * 5f); // Move up 5 units (in 2D)
            counter++; // add to the counter
        }
        else
        {
            Destroy(gameObject); // Self-destruct when done with else ifs
        }
    }
}
