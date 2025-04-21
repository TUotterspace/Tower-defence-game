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
    public Slider chargebar;

    [Header("Recharge Settings")]
    public float rechargeAmountPerSecond = 15f;
    public float rechargeRange = 3f;
    private GameObject player;

    [Header("UI")]
    public GameObject chargeBarPrefab;
    [SerializeField] private Image chargeBarFill;
    private Transform chargeBarCanvas;

    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip chargeSound;

    private AudioSource audioSource;
    private bool isChargingFromPlayer = false;

    private float fireCooldown = 0f;
    private GameObject targetEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        chargebar.value = currentCharge;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FindTargetEnemy();

        // Drain charge while firing
        if (targetEnemy != null)
        {
            currentCharge -= chargeDepletionRate * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        }

        // Recharge if player is near
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= rechargeRange)
        {
            if (currentCharge < maxCharge)
            {
                currentCharge += rechargeAmountPerSecond * Time.deltaTime;
                currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);

                if (!isChargingFromPlayer)
                {
                    isChargingFromPlayer = true;
                    PlayChargingSound();
                }
            }
        }
        else
        {
            if (isChargingFromPlayer)
            {
                isChargingFromPlayer = false;
                StopChargingSound();
            }
        }

        // Fire rate based on charge level
        float chargeRatio = currentCharge / maxCharge;
        float currentFireRate = Mathf.Lerp(minFireRate, maxFireRate, chargeRatio);
        fireCooldown -= Time.deltaTime;

        // Update UI bar
        if (chargebar != null)
        {
            chargebar.value = currentCharge;

            if (chargeRatio > 0.6f)
                chargeBarFill.color = Color.green;
            else if (chargeRatio > 0.3f)
                chargeBarFill.color = Color.yellow;
            else
                chargeBarFill.color = Color.red;
        }

        // Attack enemy if in range
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

            // Fire sound
            if (fireSound != null && audioSource != null)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(fireSound);
            }
        }
    }

    void PlayChargingSound()
    {
        if (chargeSound != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = chargeSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void StopChargingSound()
    {
        if (audioSource != null && audioSource.isPlaying && audioSource.clip == chargeSound)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.clip = null;
        }
    }

    public void Recharge(float amount)
    {
        currentCharge += amount;
        currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
        chargebar.value = currentCharge;
    }
}