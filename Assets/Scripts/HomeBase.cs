using UnityEngine;
using TMPro; // Add this if you're using TMP

public class HomeBase : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public TMP_Text healthText; // Or public Text if using legacy UI

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Home Base Destroyed – Game Over triggered!");
            GameOverManager.Instance.TriggerGameOver();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Base HP: " + Mathf.Max(currentHealth, 0); // keep it from going negative visually
        }
    }
}