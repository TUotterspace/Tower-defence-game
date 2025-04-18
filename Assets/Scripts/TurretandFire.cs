using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretAndFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float detectionRange = 10f;

    [Header("Charge System")]
    public float maxCharge = 100f;
    public float currentCharge = 100f;
    public float chargeDepletionRate = 5f;
    public float minFireRate = 2f;
    public float maxFireRate = 0.5f;

    [Header("Recharge Settings")]
    public float rechargeAmountPerSecond = 15f;
    public float rechargeRange = 3f;
    private GameObject player;

    [Header("UI")]
    public GameObject chargeBarPrefab;          // Assign your ChargeBar prefab in the inspector
    private Image chargeBarFill;                // The "Fill" image in the prefab
    private Transform chargeBarCanvas;          // The instantiated canvas

    private float fireCooldown = 0f;
    private GameObject targetEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (chargeBarPrefab != null)
        {
            GameObject bar = Instantiate(chargeBarPrefab, transform.position + new Vector3(0, 1.2f, 0), Quaternion.identity, transform);
            chargeBarCanvas = bar.transform;
            chargeBarFill = bar.transform.Find("Fill").GetComponent<Image>();
        }
    }

    void Update()
    {
        FindTargetEnemy();

        // Drain charge
        if (targetEnemy != null)
        {
            currentCharge -= chargeDepletionRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        }

        // Auto recharge if player is nearby
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= rechargeRange)
        {
            currentCharge += rechargeAmountPerSecond * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        }

        // Update fire rate based on charge
        float chargeRatio = currentCharge / maxCharge;
        float currentFireRate = Mathf.Lerp(minFireRate, maxFireRate, chargeRatio);
        fireCooldown -= Time.deltaTime;

        // Update UI charge bar
        if (chargeBarFill != null)
        {
            chargeBarFill.fillAmount = chargeRatio;

            // Optional color changes
            if (chargeRatio > 0.6f)
                chargeBarFill.color = Color.green;
            else if (chargeRatio > 0.3f)
                chargeBarFill.color = Color.yellow;
            else
                chargeBarFill.color = Color.red;
        }

        // Attack
        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) <= detectionRange)
        {
            RotateTowardTarget();

            if (fireCooldown <= 0f && currentCharge > 0f)
            {
                FireAtTarget();
                fireCooldown = currentFireRate;
            }
        }
    }

    void FindTargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= detectionRange)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        targetEnemy = closestEnemy;
    }

    void RotateTowardTarget()
    {
        Vector3 direction = targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void FireAtTarget()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    // Optional: Manual recharge if you want to reintroduce player interaction later
    public void Recharge(float amount)
    {
        currentCharge += amount;
        currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
    }
}