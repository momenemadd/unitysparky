using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioClip coin1;
    public AudioClip coin2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.score++;
            AudioManager.Instance.PlayRandomSFX(new AudioClip[] { coin1, coin2 });
            Destroy(gameObject);
        }
    }
}

