using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBall()
    {
        Loader.Load(Loader.Scene.BB);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }

    public void Lobby()
    {
        Loader.Load(Loader.Scene.Lobby);
    }
}
