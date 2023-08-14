using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField] GameObject pauseMenu;

    public AudioSource audioSource;
    public AudioClip pressingSound;

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
        Debug.Log("pressed");
        Time.timeScale = 1.0f;
        StartCoroutine("LoadMainMenu");
    }

    public IEnumerator LoadMainMenu() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        Debug.Log("LoadMain");
        SceneManager.LoadScene("Main Menu");
    }
    public void SelectNextLevel() {
        StartCoroutine("SelectingNextLevel");
    }

    public void BackupToMainMenu() {
        Debug.Log("pressed");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

        public IEnumerator SelectingNextLevel() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Level Select");
    }

    public void Retry() {

        Scene currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(currentScene.name);
    }
}
