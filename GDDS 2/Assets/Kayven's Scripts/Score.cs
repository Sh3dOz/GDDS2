using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public float nextScoreDeduction = 1.0f;
    public Text scoreText;
    public float timer;
    public float maxScore = 100000;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        scoreText.text = "Score = " + maxScore.ToString();

        timer += Time.deltaTime;
        if (timer > nextScoreDeduction) {
            timer = 0;
            maxScore -= 100;
        }
    }
}
