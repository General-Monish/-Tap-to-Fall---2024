using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gmanager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate=1f;
    public TextMeshProUGUI scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRandomTargets());
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator SpawnRandomTargets()
    {

        while (true)
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
}
