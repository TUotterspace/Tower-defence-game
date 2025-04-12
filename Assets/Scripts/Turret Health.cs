using UnityEngine;

using UnityEngine;

public class TurretHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Max health of the turret
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;  // Initialize the turret's health
    }

    // Method to handle damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;  // Decrease health by damage amount
        Debug.Log("Turret took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();  // Call Die() when health reaches 0 or below
        }
    }

    // Method to heal the turret
    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); // Heal, but don't exceed max health
        Debug.Log("Turret healed. Current Health: " + currentHealth);
    }

    // Handle turret destruction or deactivation
    private void Die()
    {
        Debug.Log("Turret destroyed!");
        // You can add any effects or animations for when the turret dies here.
        Destroy(gameObject);  // Destroy the turret object
    }
}