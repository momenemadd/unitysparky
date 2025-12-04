using UnityEngine;

public class ElectricBolt : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Hit the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by electric bolt!");

            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                Debug.Log($"ElectricBolt: Applying damage {damage} to player (respecting immunity)");
                stats.TakeDamage(damage);
            }

            // Destroy the bolt after it hits the player to avoid multiple hits
            Destroy(gameObject);
            return;
        }

        // Hit a wall, ground, or any collider that is NOT a trigger and not the player
        if (!other.isTrigger && !other.CompareTag("Player"))
        {
            Destroy(gameObject);    // projectile ends when it touches environment
        }
    }
}