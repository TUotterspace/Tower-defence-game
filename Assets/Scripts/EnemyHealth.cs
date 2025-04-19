using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public int creditValue = 50; // Customize per enemy

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameStats.Instance.AddKill();
        GameStats.Instance.AddCredits(creditValue);

        Destroy(gameObject); // You can replace this with death animation, etc.
    }
}
