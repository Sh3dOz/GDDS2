using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundIncrement : MonoBehaviour {

    public AudioClip coinSound;
    public float pitchIncrement;
    public float timeToResetPitch = 2f;

    private int collectedCoins;
    private float timeSinceLastCoinCollected;
    public AudioSource audioS;

    public bool incrementStarted;

    void Start() {
        pitchIncrement = Mathf.Pow(2f, 1.0f / 12f);
    }

    void Update() {
        // Check if time threshold has passed since the last collection
        if (timeSinceLastCoinCollected >= timeToResetPitch) {
            collectedCoins = 0;
        }

        // Check if collected coins count has increased
        if (collectedCoins > 0) {

        }

        timeSinceLastCoinCollected += Time.deltaTime;
    }

    public void IncrementCoinsCount() {
        collectedCoins++;
        StartCoroutine("CoinSounds");
        timeSinceLastCoinCollected = 0f;
    }

    public IEnumerator CoinSounds() {
        // Increase pitch based on collected coins count
        if (incrementStarted) {
            yield break;
        }
        incrementStarted = true;
        yield return new WaitForSeconds(0.1f);
        float newPitch = Mathf.Min(1 + (Mathf.Pow(pitchIncrement, collectedCoins) - 1), 3f);
        audioS.pitch = newPitch;
        audioS.PlayOneShot(coinSound);
        timeSinceLastCoinCollected = 0f;
        yield return new WaitForSeconds(0.1f);
        incrementStarted = false;
    }
}
