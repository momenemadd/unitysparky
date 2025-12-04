using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Swing Settings")]
    [Tooltip("Maximum angle from the center (in degrees)")]
    public float swingAmplitude = 45f;

    [Tooltip("Full swings per second (0.5 = one swing every 2 seconds)")]
    public float swingFrequency = 0.5f;

    [Tooltip("Offset if you have multiple pendulums")]
    public float phaseOffset = 0f;

    private float baseAngle;

    [Header("Damage Settings")]
    public int damage = 1;   // damage dealt to the player

    private void Start()
    {
        // if you rotate in editor, we keep that as the center angle
        baseAngle = transform.localEulerAngles.z;
    }

    private void Update()
    {
        // sine-based smooth swing
        float angle =
            baseAngle +
            Mathf.Sin((Time.time + phaseOffset) * swingFrequency * Mathf.PI * 2f)
            * swingAmplitude;

        // rotate around Z – top stays fixed because pivot is at top
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    // Damage once when the player first touches the pendulum (avoids per-frame repeated hits)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (stats != null)
        {
            Debug.Log($"Pendulum: Player entered trigger (isImmune={stats.isImmune}, health={stats.health})");
            stats.TakeDamage(damage);
        }
    }
}