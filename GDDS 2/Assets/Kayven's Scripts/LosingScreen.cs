using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LosingScreen : MonoBehaviour {
    public PlayerController player;
    public GameObject losingScreens;
    public AudioSource UI;
    public AudioClip crash;
    public AudioSource levelMusic;
    public bool deadedCoStarted = false;
    public LevelManager manager;
    public Slider progressSlider;

    public GameObject enemies;

    public Text coinsCollectedText;
    public float coinsCollectedForTotal;
    public float coinsCollected;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<PlayerController>();
        manager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {
        coinsCollected = manager.coinCount;
        if (!manager.isAlive && !deadedCoStarted) {
            StartCoroutine(LosingScreene());
            deadedCoStarted = true;
        }
    }

    public IEnumerator LosingScreene() {
        levelMusic.Stop();
        enemies.SetActive(false);

        coinsCollectedForTotal = PlayerPrefs.GetFloat("Coins", 0f); // Retrieve previously stored coins collected value

        coinsCollectedForTotal += manager.coinCount; // Add current coin count to previously stored value

        // Store the updated coins collected value
        PlayerPrefs.SetFloat("Coins", coinsCollectedForTotal);
        PlayerPrefs.SetFloat("Distance", PlayerPrefs.GetFloat("Distance") + progressSlider.value);

        coinsCollectedText.text = "Coins Collected: " + coinsCollected;

        yield return new WaitForSeconds(0.2f);
        UI.PlayOneShot(crash);
        losingScreens.SetActive(true);
    }

    public IEnumerator Crash() {
        if (deadedCoStarted) {
            yield break;
        }
        UI.PlayOneShot(crash);
        deadedCoStarted = true;
    }

}
