using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public TemporaryLilStomp player;

    [Header("Time for score to decrease")]
    private float nextScoreDeduction = 0.1f;
    private float deductionIsSlower = 10f;

    [Header("Score")]
    public Text scoreText;  
    public float maxScore = 100000f;
    public float currentScore;

    [Header("Timer")]
    private float timer;
    private float timer2;

    [Header("Score Deducted")]
    public float scoreDeducted = 126f;
    public float newScoreDeducted = 42f;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<TemporaryLilStomp>();
        currentScore = maxScore; // Have current score start with max score
    }

    // Update is called once per frame
    void Update() {

        scoreText.text = "Score = " + currentScore.ToString();

        currentScore = maxScore;

        timer += Time.deltaTime;
        timer2 += Time.deltaTime;

        if (player.isWin == false) {
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
}
