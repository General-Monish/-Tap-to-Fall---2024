using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    private void Start()
    {
        // Initialize UI state
        UpdateMusicButton();
        UpdateSfxButton();

        // Assign button click listeners
        musicToggleButton.onClick.AddListener(OnMusicToggle);
        sfxToggleButton.onClick.AddListener(OnSfxToggle);
    }

    private void OnMusicToggle()
    {
        SoundManager.Instance.ToggleMusic();
        UpdateMusicButton();
    }

    private void OnSfxToggle()
    {
        SoundManager.Instance.ToggleSFX();
        UpdateSfxButton();
    }

    private void UpdateMusicButton()
    {
        if (SoundManager.Instance.IsMusicMuted())
        {
            musicToggleButton.image.sprite = musicOffSprite;
        }
        else
        {
            musicToggleButton.image.sprite = musicOnSprite;
        }
    }

    private void UpdateSfxButton()
    {
        if (SoundManager.Instance.IsSfxMuted())
        {
            sfxToggleButton.image.sprite = sfxOffSprite;
        }
        else
        {
            sfxToggleButton.image.sprite = sfxOnSprite;
        }
    }
}
