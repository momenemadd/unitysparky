using UnityEngine;

public class SentryControl : MonoBehaviour
{
    [Header("Patrol")]
    public Transform patrolPointA;
    public Transform patrolPointB;
    public float moveSpeed = 2f;

    [Header("Detection & Attack")]
    public Transform player;
    public float detectionRange = 6f; // when to show warning
    public float attackRange = 4f;    // when to shoot
    public float fireRate = 1f;       // shots per second

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;

    private Animator anim;
    private Vector3 currentTarget;
    private float fireCooldown;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        // Find player automatically if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (patrolPointA != null && patrolPointB != null)
            currentTarget = patrolPointB.position;
    }

    private void Update()
    {
        if (player == null) return;

        fireCooldown -= Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        bool playerInDetection = distanceToPlayer <= detectionRange;
        bool playerInAttack = distanceToPlayer <= attackRange;

        if (!playerInDetection)
        {
            // ---- PATROL MODE ----
            Patrol();
            anim.SetBool("IsAlert", false);
        }
        else
        {
            // ---- ALERT MODE ----
            anim.SetBool("IsAlert", true);
            anim.SetBool("IsWalking", false);

            // Face the player
            FaceTarget(player.position);

            // ---- ATTACK MODE ----
            if (playerInAttack && fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    private void Patrol()
    {
        if (patrolPointA == null || patrolPointB == null) return;

        anim.SetBool("IsWalking", true);

        // Move toward the current patrol target
        transform.position = Vector2.MoveTowards(
            transform.position,
            currentTarget,
            moveSpeed * Time.deltaTime
        );

        FaceTarget(currentTarget);

        // Switch target when close enough
        if (Vector2.Distance(transform.position, currentTarget) < 0.05f)
        {
            if (currentTarget == patrolPointA.position)
                currentTarget = patrolPointB.position;
            else
                currentTarget = patrolPointA.position;
        }
    }

    private void FaceTarget(Vector3 target)
    {
        if (target.x > transform.position.x)
            transform.localScale = new Vector3(1f, 1f, 1f);  // facing right
        else
            transform.localScale = new Vector3(-1f, 1f, 1f); // facing left
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        anim.SetTrigger("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - firePoint.position).normalized;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = dir * bulletSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}