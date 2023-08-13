using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour {

    public Text score;
    public PlayerController player;
    public LevelManager manager;

    public Score scoreCounter;

    float scoreMultipler = 1f;
    public float targetScore = 9000;
    public float scoreWithCoins;
    public float scoreWithHealth;

    public float coinScore = 400;
    public float healthScore = 1500;

    public float coinsCollected;
    public Text coinsCollectedText;
    public float coinsCollectedForTotal;
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
    public GameObject nextButton;
    public float quitTime = 0.3f;

    public float duration = 1f;
    public Text coinTexts;
    public Text healthTexts;

    public AudioSource levelMusic;
    public AudioClip winMusic;
    public bool deadedCoStarted = false;
    public bool countingStarted = false;
    public AudioSource UI;
    public GameObject uiElements;

    public string nextLevel;
    public GameObject shopPanel;
    void Start() {
        if (PlayerPrefs.GetString("Character") == "Korg")
        {
            if (PlayerPrefs.GetInt("KorgPassive") == 1)
            {
                scoreMultipler = 1.1f;
            }
            else if (PlayerPrefs.GetInt("KorgPassive") == 2)
            {
                scoreMultipler = 1.2f;
            }
            else if (PlayerPrefs.GetInt("KorgPassive") == 3)
            {
                scoreMultipler = 1.3f;
            }
        }

        Debug.Log(scoreMultipler);
        manager = FindObjectOfType<LevelManager>();

        player = FindObjectOfType<PlayerController>();

        scoreCounter = FindObjectOfType<Score>();

        coinsCollected = manager.coinCount;

        if (LevelManager.playerSpawned)
        {
            healthLeft = player.health;
        }

        // Calculate initial target score and related values
        targetScore = scoreCounter.currentScore * scoreMultipler;
        scoreWithCoins = targetScore + (coinsCollected * coinScore);
        scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);
    }

    void Update() {

        // Update target score and related values if needed
        if (manager.isWin) {
            if(player == null)
            {
                player = FindObjectOfType<PlayerController>();
            }
            healthLeft = player.health;
            coinsCollected = manager.coinCount;
            if (PlayerPrefs.GetString("Character") == "X")
            {
                if (PlayerPrefs.GetInt("XPassive") == 1)
                {
                    coinsCollected *= 1.1f;
                }
                else if (PlayerPrefs.GetInt("XPassive") == 2)
                {
                    coinsCollected *= 1.2f;
                }
                else if (PlayerPrefs.GetInt("XPassive") == 3)
                {
                    coinsCollected *= 1.3f;
                }
            }
            coinsCollectedText.text = "Coins Collected: " + manager.coinCount;
            healthLeftText.text = "Health Left: " + player.health;
            incrementSpeed = targetScore / timeToCalculate;

            incrementSpeedForCoins = scoreWithCoins / timeToCalculate;
            incrementSpeedForHealth = scoreWithHealth / timeToCalculate;

            

            targetScore = scoreCounter.currentScore * scoreMultipler;
            scoreWithCoins = targetScore + (coinsCollected * coinScore);
            scoreWithHealth = targetScore + (coinsCollected * coinScore) + (healthLeft * healthScore);



            StartCoroutine("WinMusic");

            StartCoroutine("OpenResults");

            StartCoroutine("CoinCounting");


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

    public void Continue() {
        StartCoroutine("NextLevel");
    }

    public IEnumerator NextLevel() {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("LoadMain");
        SceneManager.LoadScene(nextLevel);
    }

        public IEnumerator WinMusic() {
        levelMusic.Stop();
        if (deadedCoStarted) {
            yield break;
        }
        deadedCoStarted = true;
        UI.PlayOneShot(winMusic);
    }

    public IEnumerator CoinCounting() {
        if (countingStarted) {
            yield break;
        }
        countingStarted = true;

        coinsCollectedForTotal = PlayerPrefs.GetFloat("Coins", 0f); // Retrieve previously stored coins collected value

        coinsCollectedForTotal += manager.coinCount; // Add current coin count to previously stored value

        // Store the updated coins collected value
        PlayerPrefs.SetFloat("Coins", coinsCollectedForTotal);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator OpenResults() {
        uiElements.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        resultsScreen.SetActive(true);
        StartCoroutine("AddScore");
    }

    private IEnumerator AddScore() {

        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad <= targetScore) {

            currentScoreLoad += (int)incrementSpeed * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, targetScore); // Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForCoins() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment


        while (currentScoreLoad <= scoreWithCoins){
            currentScoreLoad += (int)incrementSpeedForCoins * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithCoins);   // Clamp the score to the target value.

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }


    private IEnumerator AddScoreForHealth() {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the score increment

        while (currentScoreLoad <= scoreWithHealth){
            currentScoreLoad += (int)incrementSpeedForHealth * Time.deltaTime;
            currentScoreLoad = Mathf.Min(currentScoreLoad, scoreWithHealth);// Clamp the score to the target value

            score.text = currentScoreLoad.ToString();

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator CanExit() {
        yield return new WaitForSeconds(quitTime);
        nextButton.SetActive(true);
        quitButton.SetActive(true);  
        if(manager.activateShop == true)
        {
            shopPanel.SetActive(true);
            PlayerPrefs.SetInt("Shop", 1);
        }
    }
}