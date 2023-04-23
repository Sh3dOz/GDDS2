using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
    }
    // Load game.
    public void NewGame() {
        SceneManager.LoadScene("Level 01");
    }
    public void Credits() {
        SceneManager.LoadScene("Credits");
    }
    public void Settings() {
        SceneManager.LoadScene("Settings");
    }

    public void Instructions() {
        SceneManager.LoadScene("Instructions");
    }
    // Quit game.
    public void QuitGame() {
        Application.Quit();
    }
}
