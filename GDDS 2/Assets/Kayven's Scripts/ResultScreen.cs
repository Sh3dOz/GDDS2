using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {
    public Text scoreText;
    public int targetScore = 9000;
    public int scoreWithCoins;
    public int scoreWithHealth;

    public int coinScore = 400;
    public int healthScore = 1500;

    public int coinsCollected = 4;
    public int healthLeft = 5;
    public float incrementSpeed = 100f;
    public float incrementSpeedForCoins = 900f;
    public float incrementSpeedForHealth = 1500f;
    public bool firstCalculation = false;
    public bool secondCalculation = false;

    private int currentScore = 0;


    public void Start() {
        StartCoroutine("AddScore");
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);
    }

    private void Update() {

        if (currentScore >= targetScore) { 
            firstCalculation = true;
            StartCoroutine("AddScoreForCoins");
        }

        if (currentScore >= scoreWithCoins) { 
            secondCalculation = true;
            StartCoroutine("AddScoreForHealth");
        }

    }

    private IEnumerator AddScore() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScore < targetScore) {
            currentScore += (int)(incrementSpeed * Time.deltaTime);
            currentScore = Mathf.Min(currentScore, targetScore); // Clamp the score to the target value

            scoreText.text = currentScore.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForCoins() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScore < scoreWithCoins){
            currentScore += (int)(incrementSpeedForCoins * Time.deltaTime);
            currentScore = Mathf.Min(currentScore, scoreWithCoins);   // Clamp the score to the target value.

            scoreText.text = currentScore.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForHealth() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScore < scoreWithHealth){
            currentScore += (int)(incrementSpeedForHealth * Time.deltaTime);
            currentScore = Mathf.Min(currentScore, scoreWithHealth);// Clamp the score to the target value

            scoreText.text = currentScore.ToString();

            yield return null; // Wait for the next frame
        }
    }

}