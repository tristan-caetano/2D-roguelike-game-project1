// Samuel Rouillard, Tristan Caetano, Elijah Karpf
// Descend Project
// CIS 464 Project 1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    GameObject mainMenuInstance;
    GameObject settingsMenuInstance;
    GameObject controlsMenuInstance;

    void Update() {
    if(mainMenuInstance == null) {
        mainMenuInstance = GameObject.Find("MainMenu");
    } else {
            if (!mainMenuInstance.activeSelf && Input.GetKeyDown(KeyCode.Escape)) {
                if(GameIsPaused) {
                    Resume();
                } else {
                    Pause();
                }
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().Play("Click");
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<AudioManager>().Play("Click");
    }

    // 
    public void PauseQuitGame() {
        // Pause menu "quit" button will not quit the game, it will quit the
        // specific run and bring the player back to main menu.
        // The main menu "quit" will quit the entire game.
        Debug.Log("Quitting game...");
        pauseMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Mapping");
    }

}