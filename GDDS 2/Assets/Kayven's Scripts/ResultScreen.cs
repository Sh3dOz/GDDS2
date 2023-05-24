using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

    public Text score;
    public TemporaryLilStomp player;

    public Score scoreCounter;

    public float targetScore = 9000;
    public float scoreWithCoins;
    public float scoreWithHealth;

    public float coinScore = 400;
    public float healthScore = 1500;

    public float coinsCollected;
    public float healthLeft;
    public float incrementSpeed;
    public float incrementSpeedForCoins;
    public float incrementSpeedForHealth;
    public bool firstCalculation = false;
    public bool secondCalculation = false;

    private float currentScoreLoad = 0;

    public GameObject resultsScreen;


    void Start() {

        player = FindObjectOfType<TemporaryLilStomp>();

        scoreCounter = FindObjectOfType<Score>();
        
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);
    }

    void Update() {

        if(player.isWin) {

            targetScore = scoreCounter.currentScore;
            scoreWithCoins = targetScore + (coinsCollected * coinScore);
            scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);

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
            currentScoreLoad += (incrementSpeed * Time.deltaTime);
            currentScoreLoad = Mathf.Min(currentScoreLoad, targetScore); // Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForCoins() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad < scoreWithCoins){
            currentScoreLoad += (int)(incrementSpeedForCoins * Time.deltaTime);
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithCoins);   // Clamp the score to the target value.

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForHealth() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad < scoreWithHealth){
            currentScoreLoad += (int)(incrementSpeedForHealth * Time.deltaTime);
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithHealth);// Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }

}