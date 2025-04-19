using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageToBase = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Homebase"))
        {
            HomeBase baseScript = other.GetComponent<HomeBase>();
            if (baseScript != null)
            {
                baseScript.TakeDamage(damageToBase);
            }

            Destroy(gameObject); // Enemy disappears after hitting the base
        }
    }
}