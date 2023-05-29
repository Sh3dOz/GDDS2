using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    public bool isAlive;
    public bool isWin;
    public PlayerController player;
    public Slider progressSlider;
    public Slider healthSlider;
    public Transform startPos;
    public Transform endPos;

    public int coinCount;
    public Text coinText;
    public AudioSource coinSound;

    // Start is called before the first frame update
    void Start() {
        progressSlider.maxValue = Mathf.Abs(endPos.position.x - startPos.position.x);
        progressSlider.value = 0;
        player = FindObjectOfType<PlayerController>();
        healthSlider.value = player.health;
    }

    // Update is called once per frame
    void Update() {
        progressCheck();
    }

    void progressCheck() {
        progressSlider.value = Mathf.Abs(player.gameObject.transform.position.x - startPos.position.x);
        if (progressSlider.value == progressSlider.maxValue) {
            isWin = true;
        }
    }

    public void AddCoins(int coinsToAdd) {
        coinCount += coinsToAdd;
        coinText.text = "= " + coinCount;
        coinSound.Play();
        //PlayerPrefs.SetInt("CoinCount", coinCount);

    }
}