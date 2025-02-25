using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretandFire : MonoBehaviour
{
    public GameObject bulletPrefab;       // The bullet prefab to be fired
    public Transform firePoint;           // The point from which bullets will be fired
    public float fireRate = 1f;           // How often the turret fires (in seconds)
    private float fireCooldown = 0f;      // Timer to control fire rate
    public float detectionRange = 10f;    // Range within which the turret detects enemies
    public float maxAngle = 45f;          // Max angle the turret can rotate to target the enemy (for limited range of rotation)

    private GameObject targetEnemy;       // The current target enemy

    void Update()
    {
        // Find the closest enemy within the detection range
        FindTargetEnemy();

        // Decrease fireCooldown over time
        fireCooldown -= Time.deltaTime;

        // If there is a target and it's within range, shoot at the enemy
        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) <= detectionRange)
        {
            if (fireCooldown <= 0f)
            {
                FireAtTarget();
                fireCooldown = fireRate; // Reset cooldown to fireRate
            }
        }
    }

    // Find the closest enemy within the detection range
    void FindTargetEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Assuming your enemies have the tag "Enemy"
        float closestDistance = Mathf.Infinity;

        targetEnemy = null; // Reset target before finding the closest enemy

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance && distance <= detectionRange)
            {
                closestDistance = distance;
                targetEnemy = enemy;
            }
        }
    }

    // Function to fire bullets at the target enemy
    void FireAtTarget()
    {
        if (bulletPrefab != null && firePoint != null && targetEnemy != null)
        {
            // Calculate the direction towards the enemy
            Vector3 directionToTarget = (targetEnemy.transform.position - firePoint.position).normalized;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

            // Check if the target is within the turret's firing angle range (optional)
            if (Mathf.Abs(angle) <= maxAngle)
            {
                // Create a rotation towards the target direction
                Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);

                // Instantiate a new bullet at the firePoint's position and rotation
                Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            }
        }
    }
}
