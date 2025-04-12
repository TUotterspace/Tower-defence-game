using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float interactionRange = 3f;

    public GameObject bulletPrefab; // Drag your bullet prefab here
    public Transform firePoint;     // Empty GameObject as the bullet spawn point
    public float bulletSpeed = 10f;

    private GameObject targetTurret = null;

    void Update()
    {
        HandleMovement();
        DetectTurret();
        HandleShooting();

        if (targetTurret != null && Input.GetKeyDown(KeyCode.F))
        {
            HealTurret();
        }
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0f);

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection *= moveSpeed;
        transform.Translate(moveDirection * Time.deltaTime, Space.World);
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector2 direction = (mousePosition - firePoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }
        }
    }

    void DetectTurret()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        targetTurret = null;

        foreach (GameObject turret in turrets)
        {
            float distance = Vector3.Distance(transform.position, turret.transform.position);
            if (distance <= interactionRange)
            {
                targetTurret = turret;
            }
        }
    }

    void HealTurret()
    {
        if (targetTurret != null)
        {
            TurretHealth turretHealth = targetTurret.GetComponent<TurretHealth>();
            if (turretHealth != null)
            {
                turretHealth.Heal(20f);
                Debug.Log("Healed turret!");
            }
        }
    }
}