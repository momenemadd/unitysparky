using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("Swing Settings")]
    [Tooltip("Maximum angle from the center (in degrees)")]
    public float swingAmplitude = 45f;   // how far it swings left/right

    [Tooltip("Full swings per second (0.5 = one swing every 2 seconds)")]
    public float swingFrequency = 0.5f;  // speed of swing

    [Tooltip("Offset if you have multiple pendulums")]
    public float phaseOffset = 0f;

    private float baseAngle;

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
}