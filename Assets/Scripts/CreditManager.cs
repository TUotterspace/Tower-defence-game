using TMPro;
using UnityEngine;



public class CreditManager : MonoBehaviour
{
    public static CreditManager Instance;

    public float currentCredits = 0; // Starting credits

    public TMP_Text creditText; // Reference to TMP_Text (TextMeshPro)

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Add credits (call when enemy is killed)
    public void AddCredits(int amount)
    {
        currentCredits += amount;
        UpdateCreditsUI();
    }

    // Deduct credits (call when player spends credits at HomeBase)
    public bool SpendCredits(float amount)
    {
        if (currentCredits >= amount)
        {
            currentCredits -= amount;
            UpdateCreditsUI();
            return true; // Successfully spent
        }
        else
        {
            return false; // Not enough credits
        }
    }

    // Update the UI
    void UpdateCreditsUI()
    {
        if (creditText != null)
        {
            creditText.text = "Credits: " + currentCredits.ToString("0");
        }
    }
}