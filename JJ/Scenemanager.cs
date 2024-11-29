using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenemanager : MonoBehaviour
{
 /*   [SerializeField] Button PlayButton;
    [SerializeField] Button QuitButton;*/
    // Start is called before the first frame update
    void Start()
    {
      /*  PlayButton.onClick.AddListener(PlayButtons);
        QuitButton.onClick.AddListener(QuitBtn);*/
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayButtons()
    {
        SceneManager.LoadScene("Clicky");
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
