using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShieldMotion : MonoBehaviour
{
    [Header("Laser Movement Settings")]
    public float moveDistance = 3f;      
    public float moveSpeed = 2f;         

    private Vector3 startPos;
    private Vector3 endPos;
    private bool movingToEnd = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + transform.right * moveDistance;
        // Change to transform.up if you want vertical movement
    }

    void Update()
    {
        MoveLaser();
    }

    void MoveLaser()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, endPos) < 0.01f)
                movingToEnd = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, startPos) < 0.01f)
                movingToEnd = true;
        }
    }
}