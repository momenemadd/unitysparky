using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShieldsControl : MonoBehaviour
{
    public float moveSpeed = 2f;       // still here if you want it later
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public int damageAmount = 1;

    private Transform player;
    private bool playerDetected;
    private bool isAttacking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Detect player (but do NOT move)
        if (distance < detectionRange && distance > attackRange)
        {
            playerDetected = true;
            isAttacking = false;
            // stays in place, no MoveTowardsPlayer();
        }
        else if (distance <= attackRange)
        {
            playerDetected = true;
            isAttacking = true;

            AttackPlayer();
        }
        else
        {
            playerDetected = false;
            isAttacking = false;
        }
    }

    void MoveTowardsPlayer()
    {
        // This function still exists but is not used (Gear stays still)
        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        Debug.Log("Gear attacks player!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats stats = collision.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.TakeDamage(damageAmount);
            }
        }
    }
}
