
using System.Collections;
using UnityEngine;

public class FireFlame : MonoBehaviour
{
    [Header("Flame Scale Settings")]
 public int damage = 1;
    public float minScale = 0.0f;   // fully hidden inside tube
    public float maxScale = 1.0f;   // fully extended flame
    public float extendTime = 0.2f;
    public float retractTime = 0.2f;
    public float waitExtended = 0.5f;
    public float waitHidden = 0.5f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        // Start hidden
        SetScale(minScale);

        StartCoroutine(FlameRoutine());
    }

    private void SetScale(float s)
    {
        Vector3 newScale = originalScale;
        newScale.y = originalScale.y * s;
        transform.localScale = newScale;
    }

    private IEnumerator FlameRoutine()
    {
        while (true)
        {
            // Extend outward (flame appears)
            yield return StartCoroutine(ScaleY(minScale, maxScale, extendTime));
            yield return new WaitForSeconds(waitExtended);

            // Retract upward (flame hides inside)
            yield return StartCoroutine(ScaleY(maxScale, minScale, retractTime));
            yield return new WaitForSeconds(waitHidden);
        }
    }

    private IEnumerator ScaleY(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float v = Mathf.Lerp(from, to, t / duration);
            SetScale(v);
            yield return null;
        }

        SetScale(to);
    }
private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerStats stats = other.GetComponent<PlayerStats>();

        // Only damage when not immune (your PlayerStats handles immunity + respawn)
        if (stats != null && !stats.isImmune)
        {
            stats.TakeDamage(damage);
            Debug.Log("Player burned by fire! Health: " + stats.health);
        }
    }
}