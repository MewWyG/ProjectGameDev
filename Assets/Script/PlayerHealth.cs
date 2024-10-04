using System.Collections;
using UnityEngine;
using UnityEngine.UI; // If using a health bar

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;         // Maximum health
    private int currentHealth;          // Current health

    public Slider healthSlider;         // Reference to the health slider UI

    private Animator animator;          // Reference to the Animator component
    private bool isDead = false;        // To track if the player is dead

    private PlayerController playerController; // Reference to the PlayerController script
    private Rigidbody rb;               // Reference to Rigidbody (if applicable)

    void Start()
    {
        currentHealth = maxHealth;     // Initialize current health
        UpdateHealthSlider();

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Get the PlayerController component (assuming it's attached to the same GameObject)
        playerController = GetComponent<PlayerController>();

        // Get the Rigidbody component if movement is physics-based
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Don't take damage if already dead

        currentHealth -= damage;       // Decrease current health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Prevent negative health
        UpdateHealthSlider();

        // Set isHit to true to trigger the hit animation
        animator.SetBool("isHit", true);

        // Call a coroutine to reset the isHit flag after the animation
        StartCoroutine(ResetHitAnimation());

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
        if (isDead) return; // Prevent multiple death triggers

        isDead = true; // Set player status as dead
        animator.SetBool("isDead", true); // Trigger death animation
        Debug.Log("Player has died!");

        // Disable the PlayerController script to stop movement
        if (playerController != null)
        {
            playerController.enabled = false; // Disable movement
        }

        // Optionally disable the Rigidbody to stop any further physics-based movement
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // Stop any ongoing movement
            rb.isKinematic = true;      // Make Rigidbody kinematic (no physics interactions)
        }

        // Disable any other scripts or logic related to player actions if necessary
    }

    private IEnumerator ResetHitAnimation()
    {
        // Assuming the hit animation takes 0.5 seconds; adjust this time based on your animation
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isHit", false); // Reset the isHit flag
    }
}
