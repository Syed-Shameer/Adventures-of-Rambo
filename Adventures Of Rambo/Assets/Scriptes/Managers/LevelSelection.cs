using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelection : MonoBehaviour
{
    public SceneController sceneController;

    public void OnLevelButtonPressed(string levelName)
    {
        sceneController.LoadScene(levelName);
    }
}

