using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToSpaceship : MonoBehaviour {

    public GameObject spaceship;
    public GameObject player;
    public GameObject spaceshipDup;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            player.SetActive(false);
            spaceship.SetActive(true);
            spaceshipDup.SetActive(false);
        }
    }
}
