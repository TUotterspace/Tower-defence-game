using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Slider chargebar;
    [SerializeField] private float rechargecost = 25;
    public float maxCharge = 100f;
    public float currentCharge;
    public float rechargeSpeed = 20f; // How fast player recharges at Home Base
    private bool isNearHomeBase = false;

    void Start()
    {
        currentCharge = maxCharge;
        chargebar.value = currentCharge;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleRecharge(); 
        chargebar.value = currentCharge;  
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




                bool spent = CreditManager.Instance.SpendCredits(rechargecost * Time.deltaTime);
                if (spent)
                {
                    currentCharge += rechargeSpeed * Time.deltaTime;
                    currentCharge = Mathf.Min(currentCharge, maxCharge);
                    chargebar.value = currentCharge;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Homebase"))
        {
            isNearHomeBase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Homebase"))
        {
            isNearHomeBase = false;
        }
    }
}