using UnityEngine;

public class TurretRecharge : MonoBehaviour
{
    public float rechargeRate = 10f; // How fast turret charges per second
    public float playerChargeCostPerSecond = 10f;

    private bool isPlayerNearby = false;
    private PlayerMovement player;

    void Update()
    {
        if (isPlayerNearby && player != null && player.currentCharge > 0)
        {
            float chargeToTransfer = rechargeRate * Time.deltaTime;
            float chargeCost = playerChargeCostPerSecond * Time.deltaTime;

            if (player.currentCharge >= chargeCost)
            {
                player.currentCharge -= chargeCost;

                // You would increase the turret's internal energy here:
                // e.g., turretEnergy.currentEnergy += chargeToTransfer;

                Debug.Log("Recharging turret... +" + chargeToTransfer + " | -" + chargeCost + " from player");
            }
            else
            {
                Debug.Log("Player out of charge. Turret stops recharging.");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerMovement>();
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }
}