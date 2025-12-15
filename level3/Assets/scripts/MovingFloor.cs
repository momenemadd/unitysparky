using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    [Header("Movement Points")]
    public Transform pointA;       // First point
    public Transform pointB;       // Second point

    [Header("Settings")]
    public float speed = 2f;       // Movement speed

    private Vector3 targetPosition;

    void Start()
    {
        // Start by moving towards point B
        targetPosition = pointB.position;
    }

    void Update()
    {
        // If we are very close to point A, switch target to B
        if (Vector2.Distance(transform.position, pointA.position) < 0.05f)
        {
            targetPosition = pointB.position;
        }
        // If we are very close to point B, switch target to A
        else if (Vector2.Distance(transform.position, pointB.position) < 0.05f)
        {
            targetPosition = pointA.position;
        }

        // Move the platform towards the current target position
        transform.position = Vector2.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }

    // Keep player stuck to platform while standing on it (like in the lab) :contentReference[oaicite:3]{index=3}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    // Release the player when they jump off / leave the platform
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
