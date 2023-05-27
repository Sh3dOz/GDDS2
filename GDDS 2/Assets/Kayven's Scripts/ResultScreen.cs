using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    public Text score;
    public TemporaryLilStomp player;
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

    public GameObject resultsScreen;
    public GameObject quitButton;
    public float quitTime = 0.3f;

    public float duration = 1f;
    public Text coinTexts;
    public Text healthTexts;


    void Start() {

        

        theLevelManager = FindObjectOfType<LevelManager>();

        player = FindObjectOfType<TemporaryLilStomp>();

        scoreCounter = FindObjectOfType<Score>();

        coinsCollected = theLevelManager.coinCount;

        healthLeft = player.health;

        // Calculate initial target score and related values
        targetScore = scoreCounter.currentScore;
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);
    }

    void Update() {
        // Update target score and related values if needed
        if (player.isWin) {

            incrementSpeed = targetScore / 1000f;
            incrementSpeedForCoins = scoreWithCoins / 1000f;
            incrementSpeedForHealth = scoreWithHealth / 1000f;

            coinsCollected = theLevelManager.coinCount;

            targetScore = scoreCounter.currentScore;
            scoreWithCoins = targetScore + (coinsCollected * coinScore);
            scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);

            coinsCollectedText.text = "Coins Collected: " + theLevelManager.coinCount;
            healthLeftText.text = "Health Left: " + player.health;


            StartCoroutine("AddScore");

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

       

    public IEnumerator OpenResults() {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Opening Results Screen");
        resultsScreen.SetActive(true);
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

        coinTexts.CrossFadeAlpha(0, duration, true);
        Destroy(coinTexts, duration);

        while (currentScoreLoad < scoreWithCoins){
            currentScoreLoad += (int)incrementSpeedForCoins * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithCoins);   // Clamp the score to the target value.

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForHealth() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        healthTexts.CrossFadeAlpha(0, duration, true);
        Destroy(healthTexts, duration);

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