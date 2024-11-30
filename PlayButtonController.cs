using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    public string sceneName;  // Name of the scene for the respective game

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnApplicationQuit()
    {
        OnApplicationQuit();
    }
}
