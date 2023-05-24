using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour {

    [Header("MusicVolume")]
    public AudioMixer audioMixer;
    public Slider musicVolumeSlider;

    [Header("UIVolume")]
    public AudioMixer uiAudioMixer;
    public Slider uiVolumeSlider;
    
    public AudioSource audioSource; 
    public AudioClip pressingSound; 



    private void Start() {
        // Initialize the slider positions based on the stored volume values
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f);
        uiVolumeSlider.value = PlayerPrefs.GetFloat("VolumeUI", 0.5f);
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
        // Store the slider value in PlayerPrefs
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetUIVolume(float volume) {
        uiAudioMixer.SetFloat("VolumeUI", volume);
        // Store the slider value in PlayerPrefs
        PlayerPrefs.SetFloat("VolumeUI", volume);
    }

    public void ReturnToMainMenu() {
        StartCoroutine("BackMainMenu");
    }

    public IEnumerator BackMainMenu() {
        audioSource.PlayOneShot(pressingSound);
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Main Menu");
    }
}
