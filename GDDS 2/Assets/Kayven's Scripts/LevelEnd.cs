using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    public KorgController player;


    public void Start() {
        player = FindObjectOfType<KorgController>();    
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            player.isWin = true;
        }
    }
}
