using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Smanager gameManager;

    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Button sfxToggleButton;

    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    public GameObject pauseBtn;
    public GameObject restartBtn;
    public GameObject MMBtn;
    public GameObject pausePanel;
    public GameObject introImage;

    public Slider levelProgressBar; // Reference to the slider

    public TextMeshProUGUI countdownText; // Assign your countdown text here
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;

    public float countdownDuration = 3f; // Countdown time in seconds
    public int levelCount;
    private int scoreCount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        MMBtn.SetActive(false);
        restartBtn.SetActive(false);

        StartCoroutine(CountdownRoutine());
        // Initialize UI state
        UpdateMusicButton();
        UpdateSfxButton();

        if (levelProgressBar != null)
            levelProgressBar.value = 0f;
        levelCount = 0;

        // Assign button click listeners
        musicToggleButton.onClick.AddListener(OnMusicToggle);
        sfxToggleButton.onClick.AddListener(OnSfxToggle);
    }

    public void UpdateLevelProgress()
    {
        if (levelProgressBar != null && Smanager.Instance.totalEnemiesToSpawn > 0)
        {
            float progress = 1f - (float)Smanager.Instance.enemyCount / Smanager.Instance.totalEnemiesToSpawn;
            levelProgressBar.value = progress; // Update the progress bar value
        }
    }

    public void DisplayLevelNumber()
    {
        levelText.text = "Level: " + levelCount.ToString();
    }

    public void DisplayScore(int scorePoints)
    {
        scoreCount += scorePoints;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = "Score: " + scoreCount.ToString(); // Assume scoreText is a UI Text element
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

        if (introImage != null)
        {
            introImage.SetActive(false); // Hide the intro image
        }
    }


    public void PauseBtn()
    {
        Time.timeScale = 0.0f;
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);

        // Pause audio
        SoundManager.Instance.musicSource.Pause();
        SoundManager.Instance.sfxSource.Pause();

        // Disable physics-related components
        Rigidbody[] allRigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        /* // Pause audio
         AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
         foreach (AudioSource audio in allAudioSources)
         {
             audio.Pause();
         }*/
    }

    public void ResumeBtn()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        pauseBtn.SetActive(true);

        // Enable physics-related components
        Rigidbody[] allRigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = false;
        }

        // Resume audio
        SoundManager.Instance.musicSource.UnPause();
        SoundManager.Instance.sfxSource.UnPause();
        /*  // Resume audio
          AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
          foreach (AudioSource audio in allAudioSources)
          {
              audio.UnPause();
          }*/
    }

    public void MMbtn()
    {
        Loader.Load(Loader.Scene.MainMenu);
        Time.timeScale = 1.0f;
    }

    public void RestartBtn()
    {
        Loader.Load(Loader.Scene.BB);
    }
}
