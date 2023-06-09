using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public Vector3 rotationRate;

    public LevelManager theLevelManager;
    public int coinValue;

    public CoinSoundIncrement coinManager;

    // Start is called before the first frame update
    void Start() {
        theLevelManager = FindObjectOfType<LevelManager>();
        coinManager = FindObjectOfType<CoinSoundIncrement>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(rotationRate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            theLevelManager.AddCoins(coinValue);
            coinManager.IncrementCoinsCount();
            Destroy(gameObject);
        }

    }

}
