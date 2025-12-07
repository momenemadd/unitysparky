using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
{
    // Detect PlayerStats even if collider belongs to a child object
    PlayerStats stats = other.GetComponent<PlayerStats>() ?? other.GetComponentInParent<PlayerStats>();

    if (stats != null)
    {
        Debug.Log("Player hit by HomingBullet!");

        if (!stats.isImmune)
            stats.TakeDamage(damage);

        Destroy(gameObject); // remove bullet
        return;
    }

    // Hit wall / environment
    if (!other.isTrigger)
    {
        Destroy(gameObject);
    }
}

}