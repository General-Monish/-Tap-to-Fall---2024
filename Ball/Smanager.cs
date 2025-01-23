using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Smanager : MonoBehaviour
{
    public static Smanager Instance;

    [SerializeField] GameObject powerupPrefab;
    [SerializeField] GameObject enemeyPrefab;


    public float totalEnemiesToSpawn; // Total enemies in a wave
    private PlayerController playerController;
    float spawnPos = 9f;
    public int enemyCount;
    public int waveNum = 5;

    public GameObject pauseBtn;
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
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        MMBtn.SetActive(false);
        restartBtn.SetActive(false);
        playerController = GameObject.FindAnyObjectByType<PlayerController>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            
            if (waveNum < 14) // level 10 set wavenum < 14
            {
                Instantiate(powerupPrefab, GenerateRandomPosition(), powerupPrefab.transform.rotation);
                waveNum++;
                totalEnemiesToSpawn = waveNum; // Update the total enemies count
                SpawnEnemyWave(waveNum);
                UIManager.instance.DisplayLevelNumber();
            }
            else
            {
                Debug.Log("Boss Enemy Spawned");
                UIManager.instance.levelProgressBar.gameObject.SetActive(false);
                UIManager.instance.levelText.gameObject.SetActive(false);
                UIManager.instance.scoreText.gameObject.SetActive(false);
            }

        }
        UIManager.instance.UpdateLevelProgress();
        GameOver();
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        totalEnemiesToSpawn = enemiesToSpawn; // Set the new total
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPosition = GenerateRandomPosition();
            Instantiate(enemeyPrefab, spawnPosition, enemeyPrefab.transform.rotation);
        }
        UIManager.instance.levelCount++;
    }

    private Vector3 GenerateRandomPosition()
    {
        float spawnX = Random.Range(-spawnPos, spawnPos);
        float spawnZ = Random.Range(-spawnPos, spawnPos);
        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);
        return randomPos;
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
