using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Role")]
    public bool isManager = false;   // TRUE on "Laser Groups", FALSE on each laser beam

    // ----- MANAGER FIELDS -----
    [Header("Group Settings (Manager Only)")]
    public Transform upwardsLasers;      // parent of upward1, upward2
    public Transform downwardsLasers;    // parent of downward1, 2, 3
    public float switchInterval = 2f;    // seconds before swapping

    // ----- DAMAGE FIELDS -----
    [Header("Damage Settings (Beam Only)")]
    public int damage = 1;               // damage dealt to player

    private bool upIsOn = true;

    private void Start()
    {
        if (isManager)
        {
            // Start with upwards ON, downwards OFF
            SetGroupActive(upwardsLasers, true);
            SetGroupActive(downwardsLasers, false);

            StartCoroutine(SwitchRoutine());
        }
    }

    // ================= MANAGER LOGIC =================
    private IEnumerator SwitchRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            upIsOn = !upIsOn;

            SetGroupActive(upwardsLasers, upIsOn);
            SetGroupActive(downwardsLasers, !upIsOn);
        }
    }

    private void SetGroupActive(Transform parent, bool active)
    {
        if (parent == null) return;

        foreach (Transform child in parent)
        {
            // Turn sprite on/off
            var sr = child.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = active;

            // Enable/disable collider so damage only works when ON
            var col = child.GetComponent<Collider2D>();
            if (col != null) col.enabled = active;
        }
    }

    // ================= DAMAGE LOGIC (for beams) =================
    private void OnTriggerStay2D(Collider2D other)
    {
        // Only beams (isManager == false) should damage
        if (isManager) return;

        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null && !stats.isImmune)
        {
            stats.TakeDamage(damage);
            Debug.Log("Player hit by laser. HP: " + stats.health);
        }
    }
}