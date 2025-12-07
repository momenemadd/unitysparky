using UnityEngine;

public class DarkOrbController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public int damageAmount = 1;

    private Transform player;
    private Animator anim;
    private bool playerDetected;
    private bool isAttacking;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        /// Detect player
        if (distance < detectionRange && distance > attackRange)
        {
            playerDetected = true;
            isAttacking = false;

            anim.SetBool("playerDetected", true);
            anim.SetBool("attacking", false);

            MoveTowardsPlayer();
        }
        else if (distance <= attackRange)
        {
            playerDetected = true;
            isAttacking = true;

            anim.SetBool("playerDetected", true);
            anim.SetBool("attacking", true);

            AttackPlayer();
        }
        else
        {
            playerDetected = false;

            anim.SetBool("playerDetected", false);
            anim.SetBool("attacking", false);
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
        // damage the player once per second
        // you can expand this based on your player health script
        Debug.Log("Dark orb attacks player!");
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
