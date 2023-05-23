using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [Header("Shield heath")]
    public float shieldHealth = 3;
    public TemporaryLilStomp player;


    // Start is called before the first frame update
    void Start() {
        shieldHealth = 3;
    }

    // Update is called once per frame
    void Update() {
        if(player.isShielded == false) {
            Destroy(gameObject);
        }
        if(shieldHealth <= 0){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.GetComponent<Bullet>()) {
            shieldHealth -= 1;
        }
    }
}
