using UnityEngine;

public class TeslaCoil: MonoBehaviour
{
    [Header("Target")]
    public Transform target;          // drag the Player here
    public float attackRange = 8f;    // how far the coil can shoot

    [Header("Attack")]
    [Tooltip("Assign the ElectricBolt prefab from the Project window (not a scene instance)")]
    public GameObject electricBoltPrefab; // the projectile to shoot
    public Transform shootPoint;          // where the bolt spawns (orb)
    public float boltSpeed = 10f;
    public float chargeTime = 0.7f;       // time to "charge" before shooting
    public float fireCooldown = 2f;       // delay between shots

    [Header("Damage")]
    public int damage = 1;                // sent to the projectile

    private float _cooldownTimer = 0f;
    private bool _isCharging = false;
    private Coroutine _firingRoutine = null;

    // Optional animator (for charge animation)
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Debug.Log("TeslaCoil: OnEnable");
    }

    private void OnDisable()
    {
        Debug.Log("TeslaCoil: OnDisable");
    }

    private void OnDestroy()
    {
        Debug.Log("TeslaCoil: OnDestroy");
    }

    // If the coil touches the player it should disappear.
    // Support both trigger and collision events for 2D physics.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            Debug.Log("TeslaCoil: OnTriggerEnter2D - hit Player, destroying coil");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.collider.CompareTag("Player"))
        {
            Debug.Log("TeslaCoil: OnCollisionEnter2D - hit Player, destroying coil");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (target == null) return;

        _cooldownTimer -= Time.deltaTime;
        float distance = Vector2.Distance(transform.position, target.position);

        bool inRange = distance <= attackRange;

        // Start a persistent firing loop while the player stays in range
        if (inRange && _firingRoutine == null)
        {
            Debug.Log($"TeslaCoil: Player entered range (d={distance:F2}). Starting firing loop.");
            _firingRoutine = StartCoroutine(FiringLoop());
        }
        else if (!inRange && _firingRoutine != null)
        {
            Debug.Log("TeslaCoil: Player left range. Stopping firing loop.");
            StopCoroutine(_firingRoutine);
            _firingRoutine = null;
            _isCharging = false;
        }
    }

    private System.Collections.IEnumerator FiringLoop()
    {
        while (true)
        {
            _isCharging = true;
            Debug.Log("TeslaCoil: FiringLoop - Charge started");

            if (animator != null)
            {
                animator.SetBool("IsCharging", true);
            }

            yield return new WaitForSeconds(chargeTime);

            if (animator != null)
            {
                animator.SetBool("IsCharging", false);
                animator.SetTrigger("Shoot");
            }

            Debug.Log("TeslaCoil: FiringLoop - Charge complete, shooting");
            Shoot();

            _cooldownTimer = fireCooldown;
            _isCharging = false;
            Debug.Log($"TeslaCoil: FiringLoop - Shot fired, waiting {fireCooldown:F2}s");

            yield return new WaitForSeconds(fireCooldown);
        }
    }

    private System.Collections.IEnumerator ChargeAndShoot()
    {
        _isCharging = true;

        Debug.Log("TeslaCoil: Charge started");
        // Start charge animation if we have an animator
        if (animator != null)
        {
            animator.SetBool("IsCharging", true);
        }

        // wait for charge time
        yield return new WaitForSeconds(chargeTime);

        // stop charge animation
        if (animator != null)
        {
            animator.SetBool("IsCharging", false);
            animator.SetTrigger("Shoot");
        }

        // Actually fire
        Debug.Log("TeslaCoil: Charge complete, attempting to shoot");
        Shoot();

        // reset cooldown
        _cooldownTimer = fireCooldown;
        _isCharging = false;
        Debug.Log($"TeslaCoil: Shot fired, cooldown set to {_cooldownTimer:F2}");
    }

    private void Shoot()
    {
        Debug.Log($"TeslaCoil: Shoot() called. prefab={(electricBoltPrefab==null?"null":electricBoltPrefab.name)}, shootPoint={(shootPoint==null?"null":shootPoint.name)}, target={(target==null?"null":target.name)}");
        if (electricBoltPrefab == null || shootPoint == null || target == null)
        {
            Debug.LogWarning("TeslaCoil: Shoot() aborted due to null reference: " +
                (electricBoltPrefab == null ? " electricBoltPrefab" : "") +
                (shootPoint == null ? " shootPoint" : "") +
                (target == null ? " target" : ""));
            return;
        }

        // spawn bolt
        GameObject boltObj = Instantiate(electricBoltPrefab, shootPoint.position, Quaternion.identity);
        if (boltObj != null)
        {
            Debug.Log($"TeslaCoil: Spawned bolt '{boltObj.name}' at {shootPoint.position}");
        }

        // direction toward player
        Vector2 dir = (target.position - shootPoint.position).normalized;

        // set velocity if we have a rigidbody
        Rigidbody2D rb = boltObj.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Ensure bolt moves even if the prefab forgot a Rigidbody2D
            rb = boltObj.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            Debug.Log("TeslaCoil: Added Rigidbody2D to bolt at runtime");
        }

        rb.velocity = dir * boltSpeed;

        // warn if no collider is present (bolt won't hit anything)
        var col = boltObj.GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogWarning("TeslaCoil: Spawned bolt has no Collider2D — it won't trigger hits.");
        }

        // tell the projectile how much damage to do
        ElectricBolt bolt = boltObj.GetComponent<ElectricBolt>();
        if (bolt != null)
        {
            bolt.damage = damage;
        }
    }

    // Draw attack range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}