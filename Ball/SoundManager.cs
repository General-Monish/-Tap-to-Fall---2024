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

    [Header("UI Elements")]
    [SerializeField] private Image musicToggleButton;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    [SerializeField] private Image sfxToggleButton;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

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

    // Play music
    public void PlayMusic(AudioClip clip)
    {
        if (!isMusicMuted)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    // Toggle music mute
    public void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.mute = isMusicMuted;

        // Update the music button image
        UpdateMusicToggleButton();
    }

    // Toggle sound effects mute
    public void ToggleSFXMute()
    {
        isSfxMuted = !isSfxMuted;
        sfxSource.mute = isSfxMuted;

        // Update the sound effects button image
        UpdateSFXToggleButton();
    }

    // Updates the music button image based on mute state
    private void UpdateMusicToggleButton()
    {
        if (isMusicMuted)
        {
            musicToggleButton.sprite = musicOffSprite;
        }
        else
        {
            musicToggleButton.sprite = musicOnSprite;
        }
    }

    // Updates the SFX button image based on mute state
    private void UpdateSFXToggleButton()
    {
        if (isSfxMuted)
        {
            sfxToggleButton.sprite = sfxOffSprite;
        }
        else
        {
            sfxToggleButton.sprite = sfxOnSprite;
        }
    }

    // Play sound effects
    public void PlaySFX(AudioClip clip)
    {
        if (!isSfxMuted)
        {
            sfxSource.PlayOneShot(clip);
        }
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
