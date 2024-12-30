using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{

   public enum Scene
    {
        BB,
        LS,
        MainMenu,
        Lobby,
    }

    private static Action onloaderCallback;

    public static void Load(Scene scene)
    {
        onloaderCallback = () => 
        {
            SceneManager.LoadScene(scene.ToString()); 
        };

        SceneManager.LoadScene(Scene.LS.ToString());

    }

    public static void LoaderCallback()
    {
        if(onloaderCallback != null)
        {
            onloaderCallback();
            onloaderCallback = null;
        }
    }
}
