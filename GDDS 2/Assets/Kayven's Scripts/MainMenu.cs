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
    public GameObject resetPanel;

    void Start() {

    }
    // Load game.
    public void NewGame() {
        StartCoroutine("StartGame");
    }

    public IEnumerator StartGame()
    {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        if (PlayerPrefs.GetInt("Main") == 1)
        {
            SceneManager.LoadScene("Level Select");
        }
        else
        {
            PlayerPrefs.SetInt("Korg", 1);
            SceneManager.LoadScene(loadLevel);
        }
    }
    public void Credits() {
        StartCoroutine("Credit");
    }
    public IEnumerator Credit() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Credits");
    }

    public void Settings() {
        StartCoroutine("Setting");
    }

    public IEnumerator Setting() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Settings");
    }

    public void Instructions() {
        StartCoroutine("Instruction");
    }

    public IEnumerator Instruction() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Instructions");
    }

    public void BackToMainMenu() {
        StartCoroutine("LoadMainMenu");
    }

    public IEnumerator LoadMainMenu() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main Menu");
    }

    public void Upgrades() {
        StartCoroutine("Upgrading");
    }

    public IEnumerator Upgrading() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Upgrades");
    }
    // Quit game.
    public void QuitGame() {
        Application.Quit();

    }

    public void EnableReset()
    {
        resetPanel.SetActive(true);
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        resetPanel.SetActive(false);
    }

    public void DisableReset()
    {
        resetPanel.SetActive(false);
    }
}
