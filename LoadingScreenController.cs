using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressText;

    // Called to load a target scene
    public static void LoadSceneWithLoadingScreen(string targetScene)
    {
        // Load the loading scene first
        SceneManager.LoadScene("LS");

        // Pass the target scene name to the loading manager
        LoadingScreenController.TargetScene = targetScene;
    }

    public static string TargetScene { get; private set; } // The target scene to load

    private void Start()
    {
        StartCoroutine(LoadTargetSceneAsync());
    }

    private IEnumerator LoadTargetSceneAsync()
    {
        // Start async operation to load the target scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(TargetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Update progress bar and text
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = $"{(progress * 100):0}%";

            // Allow scene activation once loading is done
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Loading...";
                yield return new WaitForSeconds(0f); // Optional: Add slight delay for polish
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
