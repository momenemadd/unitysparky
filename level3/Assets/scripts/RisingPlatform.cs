using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RisingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private bool movingToA;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Ensure Rigidbody2D is set to kinematic in Play as a safety
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void FixedUpdate()
    {
        // choose target
        targetPosition = movingToA ? pointA.position : pointB.position;

        // compute next position
        Vector2 newPos = Vector2.MoveTowards(rb.position,
                                            new Vector2(targetPosition.x, targetPosition.y),
                                            speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);

        // swap target when near
        if (Mathf.Abs(rb.position.y - targetPosition.y) < 0.05f)
            movingToA = !movingToA;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(transform); // optional: parent player to platform
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            col.transform.SetParent(null);
    }
}