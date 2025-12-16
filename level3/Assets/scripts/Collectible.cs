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
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayRandomSFX(new AudioClip[] { coin1, coin2 });
            else
                Debug.LogWarning("Collectible: AudioManager.Instance is null; cannot play coin SFX.");
            other.GetComponent<PlayerStats>().coins++;
            transform.root.gameObject.SetActive(false);
        }
    }
}

