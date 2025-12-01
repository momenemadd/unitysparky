using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneBar : MonoBehaviour
{
    [Header("Stretch Settings")]
    [Tooltip("Scale when retracted (near the ceiling)")]
    public float minLength = 0.1f;

    [Tooltip("Scale when fully stretched to the ground")]
    public float maxLength = 1.0f;

    [Tooltip("How fast it stretches down")]
    public float extendTime = 0.2f;

    [Tooltip("How fast it goes back up")]
    public float retractTime = 0.2f;

    [Tooltip("Wait time while retracted at the top")]
    public float waitAtTop = 1.0f;

    [Tooltip("Wait time while fully stretched at the bottom")]
    public float waitAtBottom = 0.5f;

    private Vector3 originalScale;

    private void Start()
    {
        // Save original scale
        originalScale = transform.localScale;

        // Start retracted (short)
        SetLength(minLength);

        // Start the stretch / retract loop
        StartCoroutine(CraneRoutine());
    }

    private void SetLength(float lengthMultiplier)
    {
        Vector3 s = originalScale;
        s.y = originalScale.y * lengthMultiplier;
        transform.localScale = s;
    }

    private IEnumerator CraneRoutine()
    {
        while (true)
        {
            // Stay retracted at the top
            SetLength(minLength);
            yield return new WaitForSeconds(waitAtTop);

            // Stretch down
            yield return StartCoroutine(ScaleY(minLength, maxLength, extendTime));

            // Stay fully stretched at the bottom
            yield return new WaitForSeconds(waitAtBottom);

            // Go back up (retract)
            yield return StartCoroutine(ScaleY(maxLength, minLength, retractTime));
        }
    }

    private IEnumerator ScaleY(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float k = Mathf.Lerp(from, to, t / duration);
            SetLength(k);
            yield return null;
        }
        SetLength(to);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by crane bar!");

            // Here you can call your death or damage code, for example:
            // other.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}