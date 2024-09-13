using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneController sceneController;  // Reference to the SceneController script

    // Method for the Start button to load the game scene
    public void OnStartButtonPressed()
    {
        if (sceneController != null)
        {
            sceneController.LoadScene("LevelSelection");  // Assuming "LevelSelection" is the next scene
        }
        else
        {
            Debug.LogError("SceneController is not assigned.");
        }
    }

    // Method for the Options button (add your own logic for options)
    public void OnOptionsButtonPressed()
    {
        // Open options menu logic
        Debug.Log("Options Menu Pressed");
    }

    // Method for the Quit button to quit the game
    public void OnQuitButtonPressed()
    {
        if (sceneController != null)
        {
            sceneController.QuitGame();
        }
        else
        {
            Debug.LogError("SceneController is not assigned.");
        }
    }
}
