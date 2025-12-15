using UnityEngine;

public class BeamLight : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>() ?? other.GetComponentInParent<PlayerStats>();
        if (stats != null && !stats.isImmune)
        {
            stats.TakeDamage(damage);
            Debug.Log("Player hit by DRONE beam!");
        }
    }
}
