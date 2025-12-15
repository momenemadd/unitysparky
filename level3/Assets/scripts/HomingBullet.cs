using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 1;
    public float speed = 6f;
    public float lifeTime = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 direction;   // store the direction once

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;          //  make sure it's never "thrown" by gravity
    }

    private void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
            player = p.transform;

        // Lock the direction ONCE at spawn (straight shot)
        if (player != null)
        {
            direction = ((Vector2)player.position - rb.position).normalized;
        }
        else
        {
            direction = Vector2.right;
        }

        // Safety destroy
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        // Move straight in the saved direction
        rb.velocity = direction * speed;

        // Rotate to face that direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>() ?? other.GetComponentInParent<PlayerStats>();

        if (stats != null)
        {
            if (!stats.isImmune)
                stats.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}