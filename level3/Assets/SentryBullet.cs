using UnityEngine;

public class SentryBullet : MonoBehaviour
{
    public int damage = 1;
    public float maxLifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null && !stats.isImmune)
            {
                stats.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            // Hit wall / ground
            Destroy(gameObject);
        }
    }
}