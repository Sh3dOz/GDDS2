using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public Vector3 rotationRate;

    public LevelManager theLevelManager;
    public TemporaryLilStomp player;
    public int coinValue;

    // Start is called before the first frame update
    void Start() {
        theLevelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<TemporaryLilStomp>();
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(rotationRate * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            if (!player.isWin) {
                theLevelManager.AddCoins(coinValue);
                Destroy(gameObject);
            }
        }

    }

}
