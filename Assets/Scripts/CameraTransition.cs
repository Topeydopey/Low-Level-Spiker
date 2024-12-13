using UnityEngine;
using Cinemachine;
using System.Collections;
public class CameraTransition : MonoBehaviour
{
    public CinemachineVirtualCamera levelCamera; // Camera for level overview
    public CinemachineVirtualCamera playerCamera; // Camera for player view
    public GameObject playButton; // Reference to the Play button
    public GameObject uiElements; // Reference to other UI elements like sliders

    private bool gameStarted = false; // Track if the game has started

    public void StartGame()
    {
        if (!gameStarted)
        {
            gameStarted = true;

            // Transition camera focus to the player
            levelCamera.Priority = 0;
            playerCamera.Priority = 10;

            // Hide the Play button and other UI
            playButton.SetActive(false);
            uiElements.SetActive(false);

            Debug.Log("Game Started!");
        }
    }
}
