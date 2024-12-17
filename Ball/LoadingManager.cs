using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{

    // Start the asynchronous scene loading
    public void PlayBall()
    {
        LoadingScreenController.LoadSceneWithLoadingScreen("Ball");
    } 
    
    public void MM()
    {
        LoadingScreenController.LoadSceneWithLoadingScreen("MainMenu");
    }

}
