using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    [Header("Parents of Laser Groups")]
     public int damage = 1;
    public Transform upwardsLasers;     // parent of upward1 + upward2
    public Transform downwardsLasers;   // parent of downward1 + downward2 + downward3

    [Header("Timing")]
    public float switchInterval = 2f;   // seconds before swapping

    private void Start()
    {
        StartCoroutine(SwitchRoutine());
    }

    private IEnumerator SwitchRoutine()
    {
        bool upIsActive = true;

        while (true)
        {
            // Toggle groups
            SetChildrenActive(upwardsLasers, upIsActive);
            SetChildrenActive(downwardsLasers, !upIsActive);

            yield return new WaitForSeconds(switchInterval);

            // flip for next cycle
            upIsActive = !upIsActive;
        }
    }

    // Enable/disable all children inside a parent (your exact hierarchy)
    private void SetChildrenActive(Transform parent, bool state)
    {
        foreach (Transform child in parent)
        {
            child.gameObject.SetActive(state);
        }
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