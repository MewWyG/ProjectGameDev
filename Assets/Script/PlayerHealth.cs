using UnityEngine;
using UnityEngine.UI; // If using a health bar

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         // Maximum health
    private int currentHealth;          // Current health

    public Slider healthSlider;         // Reference to the health slider UI

    void Start()
    {
        currentHealth = maxHealth;     // Initialize current health
        UpdateHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;       // Decrease current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent negative health
        UpdateHealthSlider();

        if (currentHealth <= 0)
        {
            Die(); // Call die method if health is zero
        }
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // Update slider value
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Add logic for player death (e.g., game over, respawn, etc.)
    }
}