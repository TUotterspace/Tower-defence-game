using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float shotChargeCost = 10f;

    [Header("Charge System")]
    public float maxCharge = 100f;
    public float currentCharge;
    public float rechargeSpeed = 20f; // How fast player recharges at Home Base
    private bool isNearHomeBase = false;

    void Start()
    {
        currentCharge = maxCharge;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleRecharge();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentCharge >= shotChargeCost)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0f;

                Vector2 direction = (mousePosition - firePoint.position).normalized;

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

                Debug.DrawLine(firePoint.position, mousePosition, Color.red, 1f);

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * bulletSpeed;
                }

                currentCharge -= shotChargeCost;
            }
            else
            {
                Debug.Log("Not enough charge to shoot!");
            }
        }
    }

    void HandleRecharge()
    {
        if (isNearHomeBase && CreditManager.Instance != null && CreditManager.Instance.currentCredits > 0)
        {
            if (currentCharge < maxCharge)
            {
                float rechargeAmount = rechargeSpeed * Time.deltaTime;

                // Optional: credit cost per unit of charge, change multiplier as needed
                int creditCost = Mathf.CeilToInt(rechargeAmount * 0.5f);

                bool spent = CreditManager.Instance.SpendCredits(creditCost);
                if (spent)
                {
                    currentCharge += rechargeAmount;
                    currentCharge = Mathf.Min(currentCharge, maxCharge);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HomeBase"))
        {
            isNearHomeBase = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HomeBase"))
        {
            isNearHomeBase = false;
        }
    }
}