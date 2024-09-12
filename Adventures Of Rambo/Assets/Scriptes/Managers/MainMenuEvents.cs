using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneController sceneController;

    public void OnStartButtonPressed()
    {
        sceneController.LoadScene("LevelSelection");
    }

    public void OnOptionsButtonPressed()
    {
        sceneController.LoadScene("Options");
    }

    public void OnQuitButtonPressed()
    {
        sceneController.QuitGame();
    }
}
