using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour {

    public Text score;
    public PlayerController player;
    public LevelManager theLevelManager;

    public Score scoreCounter;

    public float targetScore = 9000;
    public float scoreWithCoins;
    public float scoreWithHealth;

    public float coinScore = 400;
    public float healthScore = 1500;

    public float coinsCollected;
    public Text coinsCollectedText;
    public float healthLeft;
    public Text healthLeftText;
    public float incrementSpeed;
    public float incrementSpeedForCoins;
    public float incrementSpeedForHealth;
    public bool firstCalculation = false;
    public bool secondCalculation = false;

    private float currentScoreLoad = 0;
    public float timeToCalculate = 100f;

    public GameObject resultsScreen;
    public GameObject quitButton;
    public float quitTime = 0.3f;

    public float duration = 1f;
    public Text coinTexts;
    public Text healthTexts;

    public AudioSource levelMusic;
    public AudioClip winMusic;
    public bool deadedCoStarted = false;
    public AudioSource UI;
    public GameObject uiElements;
    public bool  firstOpened = false;

    void Start() {

        

        theLevelManager = FindObjectOfType<LevelManager>();

        player = FindObjectOfType<PlayerController>();

        scoreCounter = FindObjectOfType<Score>();

        coinsCollected = theLevelManager.coinCount;

        healthLeft = player.health;

        // Calculate initial target score and related values
        targetScore = scoreCounter.currentScore;
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);
    }

    void Update() {

        healthLeft = player.health;
        coinsCollectedText.text = "Coins Collected: " + theLevelManager.coinCount;
        healthLeftText.text = "Health Left: " + player.health;
        incrementSpeed = targetScore / timeToCalculate;

        incrementSpeedForCoins = scoreWithCoins / timeToCalculate;
        incrementSpeedForHealth = scoreWithHealth / timeToCalculate;

        coinsCollected = theLevelManager.coinCount;

        targetScore = scoreCounter.currentScore;
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);

        // Update target score and related values if needed
        if (theLevelManager.isWin && firstOpened == false) {



            StartCoroutine("WinMusic");

            StartCoroutine("OpenResults");


            if (currentScoreLoad >= targetScore) {
                firstCalculation = true;
                StartCoroutine("AddScoreForCoins");
            }

            if (currentScoreLoad >= scoreWithCoins) {
                secondCalculation = true;
                StartCoroutine("AddScoreForHealth");
            }

            if (currentScoreLoad >= scoreWithHealth) {
                StartCoroutine("CanExit");
            }

        }

    }

    public void screenClose() {
        StartCoroutine("CloseScreen");
    }


    public IEnumerator CloseScreen() {
            theLevelManager.isWin = false;
            yield return new WaitForSeconds(0.1f);
            resultsScreen.SetActive(false);
    }

    public IEnumerator WinMusic() {
        levelMusic.Stop();
        if (deadedCoStarted) {
            yield break;
        }
        firstOpened = true;
        deadedCoStarted = true;
        UI.PlayOneShot(winMusic);
    }

    public IEnumerator OpenResults() {
        uiElements.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        resultsScreen.SetActive(true);
        StartCoroutine("AddScore");
    }

    private IEnumerator AddScore() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad < targetScore) {

            currentScoreLoad += (int)incrementSpeed * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, targetScore); // Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForCoins() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment


        while (currentScoreLoad < scoreWithCoins){
            currentScoreLoad += (int)incrementSpeedForCoins * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithCoins);   // Clamp the score to the target value.

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForHealth() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad < scoreWithHealth){
            currentScoreLoad += (int)incrementSpeedForHealth * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithHealth);// Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator CanExit() {
        yield return new WaitForSeconds(quitTime);
            quitButton.SetActive(true);       
    }
}