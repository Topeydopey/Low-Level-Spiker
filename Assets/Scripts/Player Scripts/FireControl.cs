using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject shellPrefab; // Prefab for the tank shell
    public Transform firingPoint; // Position from which the shell is fired
    public float shellSpeed = 10f; // Speed of the shell

    void Update()
    {
        // Fire the shell when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireShell();
        }
    }

    void FireShell()
    {
        // Instantiate the shell at the firing point
        GameObject shell = Instantiate(shellPrefab, firingPoint.position, firingPoint.rotation);

        // Apply velocity to the shell to move it forward
        Rigidbody2D shellBody = shell.GetComponent<Rigidbody2D>();
        if (shellBody != null)
        {
            shellBody.velocity = firingPoint.up * shellSpeed; // Move forward relative to the firing point's up direction
        }
    }
}
