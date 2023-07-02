using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    [Header("Shield heath")]
    public float shieldHealth = 3;
    public KorgController player;


    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<KorgController>();
        shieldHealth = 3;
    }

    // Update is called once per frame
    void Update() {


        if(player.canBeDamaged == true) {
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
        if(collision.GetComponent<ShootingEnemy>()) {
            shieldHealth -= 1;
        }
    }
}
