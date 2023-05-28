using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    // Start is called before the first frame update

    public string loadLevel;
    public AudioSource audioSource;
    public AudioClip pressingSound;

    void Start() {

    }
    // Load game.
    public void NewGame() {
        StartCoroutine("StartGame");
    }

    public IEnumerator StartGame() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(loadLevel);
    }
    public void Credits() {
        StartCoroutine("Credit");
    }
    public IEnumerator Credit() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Credits");
    }

    public void Settings() {
        StartCoroutine("Setting");
    }

    public IEnumerator Setting() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Settings");
    }

    public void Instructions() {
        StartCoroutine("Instruction");
    }

    public IEnumerator Instruction() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Instructions");
    }

    public void BackToMainMenu() {
        StartCoroutine("LoadMainMenu");
    }

    public IEnumerator LoadMainMenu() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Main Menu");
    }
    // Quit game.
    public void QuitGame() {
        Application.Quit();

    }
}
