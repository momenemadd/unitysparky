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
    if (Instance != null && Instance != this)
    {
        Destroy(gameObject);
        return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
}

    private void OnEnable()
    {
        // attempt to auto-assign audio sources if they were lost during a scene reload
        var sources = GetComponents<AudioSource>();
        if (musicSource == null && sources.Length > 0)
            musicSource = sources[0];
        if (sfxSource == null)
        {
            if (sources.Length > 1)
                sfxSource = sources[1];
            else if (sources.Length > 0)
                sfxSource = sources[0];
        }
        if (musicSource == null)
            Debug.LogWarning("AudioManager: musicSource is not assigned or was lost; assign one in inspector or add an AudioSource to this GameObject.");
        if (sfxSource == null)
            Debug.LogWarning("AudioManager: sfxSource is not assigned or was lost; assign one in inspector or add an AudioSource to this GameObject.");
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
        if (sfxSource == null)
        {
            Debug.LogWarning("PlayMusicSFX: sfxSource is null; cannot play clip.");
            return;
        }
        if (clip == null)
        {
            Debug.LogWarning("PlayMusicSFX: clip is null; skipping.");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    // Changes background music (level change)
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null)
        {
            Debug.LogWarning("PlayMusic: musicSource is null; cannot play music.");
            return;
        }
        if (clip == null)
        {
            Debug.LogWarning("PlayMusic: clip is null; skipping.");
            return;
        }
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Plays a random SFX from an array
    public void PlayRandomSFX(AudioClip[] clips)
{
        if (sfxSource == null)
        {
            Debug.LogWarning("PlayRandomSFX: sfxSource is null; cannot play SFX.");
            return;
        }
        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning("PlayRandomSFX: clips array is null or empty.");
            return;
        }

        // pick a non-null clip (avoid ArgumentNullException)
        int attempts = 0;
        AudioClip clip = null;
        while (attempts < clips.Length)
        {
            int index = Random.Range(0, clips.Length);
            clip = clips[index];
            if (clip != null) break;
            attempts++;
        }
        if (clip == null)
        {
            Debug.LogWarning("PlayRandomSFX: all clips are null; nothing to play.");
            return;
        }
        sfxSource.PlayOneShot(clip);
}
}
