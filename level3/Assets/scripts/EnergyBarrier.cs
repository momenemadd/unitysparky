using UnityEngine;

public class EnergyBarrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();

        if (ps != null)
        {
            PlayerStats.score++;
            Debug.Log("Collected! Score = " + PlayerStats.score);
            
            Destroy(gameObject);
        }
    }
}
