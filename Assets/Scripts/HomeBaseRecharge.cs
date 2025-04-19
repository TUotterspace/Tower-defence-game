using UnityEngine;

public class HomeBaseRecharge : MonoBehaviour
{
    public int rechargeCost = 50; // The cost to recharge the player
    public float rechargeRate = 1f; // Time interval to recharge player (in seconds)

    private bool isPlayerNear = false;
    private GameObject player;

    void Update()
    {
        // If player is near and has enough credits, automatically recharge
        if (isPlayerNear && CreditManager.Instance.currentCredits >= rechargeCost)
        {
            StartRecharging();
        }
    }

    // This method will automatically be called when the player enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure it’s the player
        {
            player = other.gameObject;
            isPlayerNear = true;
        }
    }

    // This method will be called when the player leaves the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null; // Optional: stop recharging if the player leaves
        }
    }

    // Handle the recharge logic
    private void StartRecharging()
    {
        // Check if the player has enough credits to recharge
        if (CreditManager.Instance.SpendCredits(rechargeCost))
        {
            // You can add logic here to actually recharge the player (e.g., increasing health or turret charge)
            Debug.Log("Recharging player...");

            // Optionally, you could add a sound effect or visual effect for the recharge
            // Example: player.GetComponent<PlayerHealth>().IncreaseHealth(amount);

            // Add a delay between recharges if needed
            Invoke("StartRecharging", rechargeRate); // Recharges every `rechargeRate` seconds
        }
    }
}
