using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Debug.Log("lmao pressed the key idiot");
            if(GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void PauseQuitGame() {
        // Pause menu "quit" button will not quit the game, it will quit the
        // specific run and bring the player back to main menu.
        // The main menu "quit" will quit the entire game.
        Debug.Log("Quitting game...");

        // Loading the title screen scene
        SceneManager.LoadScene("MainMenu");
    }

}
