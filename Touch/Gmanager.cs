using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gmanager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate = 1f;
    public TextMeshProUGUI scoreText;
    int score;
    public TextMeshProUGUI gameoverText;
    public bool isGameActive;
    public Button restartBtnn;
    public Button MMBtnn;


    public GameObject title;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        title.SetActive(false);
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnRandomTargets());
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnRandomTargets()
    {

        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }

    public void UpdateScore(int ScoreToAdd)
    {
        score += ScoreToAdd;
        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameoverText.gameObject.SetActive(true);
        restartBtnn.gameObject.SetActive(true);
        MMBtnn.gameObject.SetActive(true);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } 
    
    public void MMtBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
