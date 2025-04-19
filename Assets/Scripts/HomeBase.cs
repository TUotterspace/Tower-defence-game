using UnityEngine;

public class HomeBase : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("Home Base Destroyed – Game Over triggered!");
            GameOverManager.Instance.TriggerGameOver();
        }
    }
}