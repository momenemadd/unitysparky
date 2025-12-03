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

            // Your damage logic:
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);

            Destroy(gameObject);    // projectile ends here
        }

        // Hit a wall, ground, or any collider that is NOT a trigger
        if (!other.isTrigger && !other.CompareTag("Player"))
        {
            Destroy(gameObject);    // projectile ends when it touches environment
        }
    }
}