using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [SerializeField] AudioClip powerupSound;
    [SerializeField] AudioClip enemycollideSound;

    private bool isMusicMuted = false;
    private bool isSfxMuted = false;


    private void Awake()
    {
        // Singleton Pattern
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

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.mute = isMusicMuted;
    }

    public void ToggleSFX()
    {
        isSfxMuted = !isSfxMuted;
        sfxSource.mute = isSfxMuted;
    }

    public bool IsMusicMuted()
    {
        return isMusicMuted;
    }

    public bool IsSfxMuted()
    {
        return isSfxMuted;
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayerPowerupSoundEffect()
    {
        sfxSource.PlayOneShot(powerupSound);
    }

    public void CollisionSoundEffct()
    {
        sfxSource.PlayOneShot(enemycollideSound);
    }
}
