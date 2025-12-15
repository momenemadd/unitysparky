using UnityEngine;

public class Collectablee : MonoBehaviour
{
    public AudioClip coin1;
    public AudioClip coin2;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.score++;

            Debug.Log("Score: " + PlayerStats.score);

            // Play sound
            if (audioSource != null && coin1 != null)
            {
                audioSource.PlayOneShot(coin1);
            }

            Destroy(gameObject, 0.1f);
        }
    }
}
