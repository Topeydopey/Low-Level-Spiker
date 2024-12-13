using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public TextMeshProUGUI tooltipText; // Reference to the tooltip text field

    public void ShowTooltip(string message)
    {
        if (tooltipText != null)
        {
            tooltipText.text = message; // Update the tooltip text
        }
    }

    public void ClearTooltip()
    {
        if (tooltipText != null)
        {
            tooltipText.text = ""; // Clear the tooltip text
        }
    }
}
