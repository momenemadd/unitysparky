using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearController : MonoBehaviour
{
    public float moveSpeed = 2f;
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

        // Detect player
        if (distance < detectionRange && distance > attackRange)
        {
            playerDetected = true;
            isAttacking = false;

            // No animator control here, just move towards player
            MoveTowardsPlayer();
        }
        else if (distance <= attackRange)
        {
            playerDetected = true;
            isAttacking = true;

            // Attack player
            AttackPlayer();
        }
        else
        {
            playerDetected = false;
        }
    }

    void MoveTowardsPlayer()
    {
        // Move only along the X axis — keep current Y position
        Vector2 target = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    void AttackPlayer()
    {
        // Damage the player once per second
        // You can expand this based on your player health script
        Debug.Log("Gear attacks player!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit the player
        if (collision.CompareTag("Player"))
        {
            // Try to get PlayerStats from the player object
            PlayerStats stats = collision.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.TakeDamage(damageAmount);
            }
        }
    }
}
