using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Static instance so there's only one AudioManager in the whole game
    public static AudioManager Instance;

    public AudioSource musicSource; // background music
    public AudioSource sfxSource;   // sound effects

    public AudioClip overworldMusic; // level 1 music

    public AudioClip[] variousSFX;   // random SFX

    private void Awake()
    {
        // Ensure only ONE AudioManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Start playing level 1 music
        if (musicSource != null && overworldMusic != null)
        {
            musicSource.clip = overworldMusic;
            musicSource.Play();
        }
    }

    // Plays a single sound effect (coin, jump, etc.)
    public void PlayMusicSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    // Changes background music (level change)
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Plays a random SFX from an array
    public void PlayRandomSFX(AudioClip[] clips)
    {
        variousSFX = clips;
        int index = Random.Range(0, variousSFX.Length);
        sfxSource.PlayOneShot(variousSFX[index]);
    }
}
