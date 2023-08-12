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
    public GameObject godPanel;
    public GameObject shopPanel;

    void Start() {
        if(PlayerPrefs.GetInt("Shop") == 1)
        {
            shopPanel.SetActive(true);
        }
    }
    // Load game.
    public void NewGame() {
        StartCoroutine("StartGame");
    }

    public IEnumerator StartGame()
    {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(0.5f);
        if (PlayerPrefs.GetInt("PlayGame") == 1)
        {
            SceneManager.LoadScene("Level Select");
        }
        else
        {
            PlayerPrefs.SetInt("PlayGame", 1);
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
        if(PlayerPrefs.GetInt("Shop") == 1)
        {
            PlayerPrefs.SetInt("Shop" , 0);
        }
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

    public void OpenGod()
    {
        int isGod = PlayerPrefs.GetInt("God");
        if (isGod == 0)
        {
            godPanel.SetActive(true);
        }
        else if(isGod == 1)
        {
            PlayerPrefs.SetInt("God", 0);
        }
    }
    public void EnableGod()
    {
        PlayerPrefs.SetInt("God", 1);
        godPanel.SetActive(false);
    }

    public void CloseGod()
    {
        godPanel.SetActive(false);
    }
}
