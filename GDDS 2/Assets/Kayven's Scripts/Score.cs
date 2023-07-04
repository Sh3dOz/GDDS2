using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public LevelManager manager;

    [Header("Time for score to decrease")]
    private float nextScoreDeduction = 0.1f;
    private float deductionIsSlower = 60f;

    [Header("Score")]
    public Text scoreText;  
    public float maxScore = 0f;
    public float currentScore;

    [Header("Timer")]
    private float timer;
    private float timer2;
    public Text timeText;
    public float timePast;


    [Header("Score Deducted")]
    public float scoreDeducted = 34f;
    public float newScoreDeducted = 69f;

    // Start is called before the first frame update
    void Start() {
        manager = FindObjectOfType<LevelManager>();
        currentScore = maxScore; // Have current score start with max score
    }

    // Update is called once per frame
    void Update() {

        CountTime();

        scoreText.text = "Score = " + currentScore.ToString();

        currentScore = maxScore;

        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (manager.isWin == false && manager.isAlive == true) {
            if (timer > nextScoreDeduction && timer2 < deductionIsSlower) { // Score is reduced every x seconds.
                timer = 0;
                maxScore -= scoreDeducted;
                currentScore = (int)maxScore; // Update current score
                                              //Debug.Log("NormalDeductionSpeed");
            }
            if (timer > nextScoreDeduction && timer2 > deductionIsSlower) { // After a certain time passes, score reduced every x seconds will be lesser.
                timer = 0;
                maxScore -= newScoreDeducted;
                currentScore = (int)maxScore; // Update current score
                                              //Debug.Log("SlowerDeductionSpeed");
            }
        }
    }

    public void CountTime() {
        timePast = 0 + Time.timeSinceLevelLoad;
        ShowTime();
    }

    public void ShowTime() {
        int minutes;
        int seconds;

        minutes = (int)timePast / 60; // Derive minutes by dividing seconds by 60 seconds
        seconds = (int)timePast % 60; // Derive remainder after dividing by 60 seconds

        timeText.text = "Time = " + minutes.ToString() + ":" + seconds.ToString("d2");
    }
}
