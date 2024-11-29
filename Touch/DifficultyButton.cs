using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    Button Btn;
    Gmanager Gmanager;
    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        Gmanager=GameObject.Find("Gmanager").GetComponent<Gmanager>();
        Btn = GetComponent<Button>();
        Btn.onClick.AddListener(SetDifficulty);
    }

    void SetDifficulty()
    {
        Gmanager.StartGame(difficulty);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
