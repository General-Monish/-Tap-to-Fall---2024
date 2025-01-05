using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Smanager : MonoBehaviour
{
    public static Smanager Instance;
    [SerializeField] GameObject powerupPrefab;
    [SerializeField] GameObject enemeyPrefab;

    private PlayerController playerController;
    float spawnPos = 9f;
    int enemyCount;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public int levelCount;
    private int scoreCount = 0;
    public GameObject pauseBtn;

    int waveNum = 2;

    public GameObject restartBtn;
    public GameObject MMBtn;
    public GameObject pausePanel;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        levelCount = 0;
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        MMBtn.SetActive(false);
        restartBtn.SetActive(false);
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
            waveNum++;
            SpawnEnemyWave(waveNum);
            DisplayLevelNumber();
        }
        GameOver();
    }


    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            Instantiate(enemeyPrefab, spawnPosition, enemeyPrefab.transform.rotation);
        }
        Smanager.Instance.levelCount++;
    }
    // nothing
    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnPos, spawnPos);
        float spawnZ = Random.Range(-spawnPos, spawnPos);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
    }

    private void DisplayLevelNumber()
    {
        levelText.text = "Level: " + levelCount.ToString();
    }

    public void DisplayScore(int scorePoints)
    {
        scoreCount += scorePoints;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + scoreCount.ToString(); // Assume scoreText is a UI Text element
    }

    public void PauseBtn()
    {
        Time.timeScale = 0.0f;
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);

        // Disable physics-related components
        Rigidbody[] allRigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Pause audio
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.Pause();
        }
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
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in allAudioSources)
        {
            audio.UnPause();
        }
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
    /*
        public void SetPlayerController(PlayerController pc)
        {
            playerController = pc;
        }*/
    private void GameOver()
    {
        if (playerController.isGameOver)
        {
            MMBtn.SetActive(true);
            restartBtn.SetActive(true);
        }
    }
}
