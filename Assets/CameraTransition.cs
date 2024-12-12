using UnityEngine;
using Cinemachine;
using System.Collections;
public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera levelCamera; // Virtual camera for level generation
    public CinemachineVirtualCamera playerCamera; // Virtual camera focused on the player
    public float transitionDelay = 2f; // Delay before switching cameras

    void Start()
    {
        StartCoroutine(SwitchToPlayerCamera());
    }

    IEnumerator SwitchToPlayerCamera()
    {
        // Wait for level generation to complete
        yield return new WaitForSeconds(transitionDelay);

        // Lower the priority of the level camera
        levelCamera.Priority = 0;

        // Raise the priority of the player camera
        playerCamera.Priority = 10;
    }
}
