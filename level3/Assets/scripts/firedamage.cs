using UnityEngine;

public class firedamage : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player burned!");

            // Insert your player damage logic here:
            // other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}