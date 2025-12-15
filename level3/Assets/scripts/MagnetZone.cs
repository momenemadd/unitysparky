using UnityEngine;

public class MagnetZone : MonoBehaviour
{
    public float pullForce = 5f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * pullForce);
        }
    }
}
