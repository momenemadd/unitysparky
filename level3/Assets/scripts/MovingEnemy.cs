using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public Transform leftPoint; 
    public Transform rightPoint;
    public float speed = 2f;
    public int damageAmount = 1;

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = rightPoint.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            targetPosition = (targetPosition == rightPoint.position)
                ? leftPoint.position
                : rightPoint.position;
        }
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