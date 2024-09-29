using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the UI namespace

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the Slider UI element

    public void SetSlider(float amount)
    {
        healthSlider.value = amount; // Fixed the typo from headlthSlider to healthSlider
    }

    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount; // Set the maximum value for the slider
        SetSlider(amount); // Set the current value to max value
    }
}