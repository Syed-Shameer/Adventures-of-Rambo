using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public SceneController sceneController;

    public void OnLevelComplete()
    {
        sceneController.LoadNextLevel();
    }
}

