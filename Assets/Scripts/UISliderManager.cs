using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISliderManager : MonoBehaviour
{
    public Slider clusterSizeSlider;
    public Slider turnProbabilitySlider;
    public Slider sa2SpawnProbabilitySlider;
    public TextMeshProUGUI clusterSizeText;
    public TextMeshProUGUI turnProbabilityText;
    public TextMeshProUGUI sa2SpawnProbabilityText;
    public Button regenerateButton; // Button for regeneration
    public Slider squareSizeSlider;
    public TextMeshProUGUI squareSizeText;

    public Pathmaker pathmaker; // Reference to Pathmaker script
    public AudioClip buttonClickSound; // Sound for button clicks
    private AudioSource audioSource;   // AudioSource for playing button sounds

    void Start()
    {
        // Initialize AudioSource
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign initial slider values
        clusterSizeSlider.value = pathmaker.clusterSize;
        turnProbabilitySlider.value = pathmaker.turnProbability;
        sa2SpawnProbabilitySlider.value = pathmaker.sa2SpawnProbability;
        squareSizeSlider.value = pathmaker.squareSize;

        // Add listeners for sliders
        clusterSizeSlider.onValueChanged.AddListener(UpdateClusterSize);
        turnProbabilitySlider.onValueChanged.AddListener(UpdateTurnProbability);
        sa2SpawnProbabilitySlider.onValueChanged.AddListener(UpdateSA2SpawnProbability);
        squareSizeSlider.onValueChanged.AddListener(UpdateSquareSize);

        // Add listener for regenerate button with sound
        regenerateButton.onClick.AddListener(() =>
        {
            PlayButtonSound();
            RegenerateMap();
        });

        // Set slider types for integers
        clusterSizeSlider.wholeNumbers = true;
        squareSizeSlider.wholeNumbers = true;
    }

    void UpdateClusterSize(float value)
    {
        pathmaker.clusterSize = Mathf.RoundToInt(value);
        clusterSizeText.text = $"Path Density: {pathmaker.clusterSize}";
    }

    void UpdateSquareSize(float value)
    {
        pathmaker.squareSize = Mathf.RoundToInt(value);
        squareSizeText.text = $"Area Size: {pathmaker.squareSize}";
    }

    void UpdateTurnProbability(float value)
    {
        pathmaker.turnProbability = value;
        turnProbabilityText.text = $"Turn Probability: {pathmaker.turnProbability:F2}";
    }

    void UpdateSA2SpawnProbability(float value)
    {
        pathmaker.sa2SpawnProbability = value;
        sa2SpawnProbabilityText.text = $"SAM Spawn: {pathmaker.sa2SpawnProbability:F2}";
    }

    void RegenerateMap()
    {
        Debug.Log("Regenerating Map...");
        // Trigger regeneration logic here
        FindObjectOfType<GameManager>().RegenerateMap();
    }

    void PlayButtonSound()
    {
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void QuitGame()
    {
        Debug.Log("Quitting Game...");

        // Quit the application
        Application.Quit();

        // Note: In the Unity Editor, Application.Quit() does not work. Use this for testing in the Editor:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
