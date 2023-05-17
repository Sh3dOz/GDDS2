using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause() {
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    public void BackToMainMenu() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry() {

        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene.name);
    }
}
