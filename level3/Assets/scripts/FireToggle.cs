using System.Collections;
using UnityEngine;

public class FireToggle : MonoBehaviour
{
    [Header("Timing")]
    public float fireOnTime = 1.5f;   // how long fire stays ON
    public float fireOffTime = 1.5f;  // how long fire stays OFF

    private SpriteRenderer sr;
    private Collider2D col;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (true)
        {
            // Turn fire ON
            sr.enabled = true;
            col.enabled = true;
            yield return new WaitForSeconds(fireOnTime);

            // Turn fire OFF
            sr.enabled = false;
            col.enabled = false;
            yield return new WaitForSeconds(fireOffTime);
        }
    }
}