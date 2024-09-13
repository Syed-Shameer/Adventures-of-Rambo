using System.Collections;  // Needed for IEnumerator and coroutines
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject levelLoaderPrefab;  // Drag the LevelLoader prefab in the inspector
    private Animator transitionAnimator;

    private void Start()
    {
        if (levelLoaderPrefab != null)
        {
            // Instantiate the LevelLoader prefab in the scene and get the Animator component
            GameObject levelLoaderInstance = Instantiate(levelLoaderPrefab);
            
            // Assuming the Animator is on the Canvas or Image inside the prefab
            transitionAnimator = levelLoaderInstance.GetComponentInChildren<Animator>();
        }
        else
        {
            Debug.LogError("LevelLoader prefab is not assigned.");
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        if (transitionAnimator != null)
        {
            // Trigger the crossfade animation
            transitionAnimator.SetTrigger("StartTransition");

            // Wait for the animation duration (adjust this duration as needed)
            yield return new WaitForSeconds(1f);  
        }

        // Load the scene after the transition animation is completed
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
