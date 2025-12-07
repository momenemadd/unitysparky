using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
     public Transform target;            // Player
    public float smoothSpeed = 5f;
    public BoxCollider2D levelBounds;   // The collider representing the level size

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        // Auto-find player if not assigned
        if (target == null)
        {
            var player = FindObjectOfType<sparky>();
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                var byName = GameObject.Find("Scofield");
                if (byName != null) target = byName.transform;
            }
        }
    }

    void LateUpdate()
    {
        Camera cam = Camera.main;
        if (cam == null || levelBounds == null)
            return;

        // Recalculate half sizes (for zoom)
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;

        Bounds bounds = levelBounds.bounds;

        float minX = bounds.min.x + camHalfWidth;
        float maxX = bounds.max.x - camHalfWidth;
        float minY = bounds.min.y + camHalfHeight;
        float maxY = bounds.max.y - camHalfHeight;

        Vector3 desiredPos;

        // Follow target if exists
        if (target != null)
            desiredPos = new Vector3(target.position.x, target.position.y, -10f);
        else
            desiredPos = new Vector3(bounds.center.x, bounds.center.y, -10f);

        float clampedX = (minX > maxX) ? bounds.center.x : Mathf.Clamp(desiredPos.x, minX, maxX);
        float clampedY = (minY > maxY) ? bounds.center.y : Mathf.Clamp(desiredPos.y, minY, maxY);

        Vector3 smoothedPos = Vector3.Lerp(transform.position,
                                           new Vector3(clampedX, clampedY, -10f),
                                           smoothSpeed * Time.deltaTime);

        // Final bounds safety clamp
        Vector3 finalPos = smoothedPos;

        float camMinX = finalPos.x - camHalfWidth;
        float camMaxX = finalPos.x + camHalfWidth;
        float camMinY = finalPos.y - camHalfHeight;
        float camMaxY = finalPos.y + camHalfHeight;

        if (camMinX < bounds.min.x)
            finalPos.x += (bounds.min.x - camMinX);
        if (camMaxX > bounds.max.x)
            finalPos.x -= (camMaxX - bounds.max.x);
        if (camMinY < bounds.min.y)
            finalPos.y += (bounds.min.y - camMinY);
        if (camMaxY > bounds.max.y)
            finalPos.y -= (camMaxY - bounds.max.y);

        transform.position = finalPos;
    }
}


