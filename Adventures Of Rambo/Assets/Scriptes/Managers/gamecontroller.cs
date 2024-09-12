using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume game
    }

    public void OnResumeButtonPressed()
    {
        TogglePauseMenu();
    }

    public void OnMainMenuButtonPressed()
    {
        // Load Main Menu scene
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}

