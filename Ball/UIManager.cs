using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Assign your countdown text here
    public float countdownDuration = 3f; // Countdown time in seconds
    public Smanager gameManager;

    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
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

    private IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true); // Show the countdown text

        for (int i = (int)countdownDuration; i > 0; i--)
        {
            countdownText.text = i.ToString(); // Display the countdown number
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }

        countdownText.gameObject.SetActive(false); // Hide the countdown text
        StartGame(); // Call the game-start logic
    }

    void StartGame()
    {
        Debug.Log("Game Started!");
        // Add your game start logic here

        if (gameManager != null)
        {
            gameManager.enabled = true; // Start the game manager logic (spawning enemies, etc.)
        }
    }
}
