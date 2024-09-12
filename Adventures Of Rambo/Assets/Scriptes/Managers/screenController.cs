using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator transitionAnimator;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        transitionAnimator.SetTrigger("StartTransition"); // Trigger crossfade animation
        yield return new WaitForSeconds(1f); // Wait for animation duration
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadScene(SceneManager.GetSceneAt(nextSceneIndex).name);
        }
        else
        {
            Debug.LogWarning("No more levels to load.");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
