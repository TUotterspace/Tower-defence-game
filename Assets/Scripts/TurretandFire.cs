using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAndFire : MonoBehaviour
{
    public GameObject bulletPrefab;       // The bullet prefab to be fired
    public Transform firePoint;           // The point from which bullets will be fired
    public float fireRate = 1f;           // How often the turret fires (in seconds)
    public float detectionRange = 10f;    // Range within which the turret detects enemies

    private float fireCooldown = 0f;      // Timer to control fire rate
    private GameObject targetEnemy;       // The current target enemy

    void Update()
    {
        FindTargetEnemy();
        fireCooldown -= Time.deltaTime;

        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) <= detectionRange)
        {
            RotateTowardTarget();

            if (fireCooldown <= 0f)
            {
                FireAtTarget();
                fireCooldown = fireRate;
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
        transform.rotation = Quaternion.Euler(0f, 0f, angle); // Rotate turret in 2D
    }

    void FireAtTarget()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
}    