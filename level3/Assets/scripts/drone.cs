using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour
{
    [Header("Beam Object")]
    public Transform beamLight; // The light beam object (child)

    [Header("Timing")]
    public float onTime = 1f;   // Time the beam stays ON
    public float offTime = 3f;  // Time the beam stays OFF

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // Beam ON
            beamLight.gameObject.SetActive(true);
            yield return new WaitForSeconds(onTime);

            // Beam OFF
            beamLight.gameObject.SetActive(false);
            yield return new WaitForSeconds(offTime);
        }
    }
}