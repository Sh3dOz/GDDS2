using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationExplosion : MonoBehaviour {

    public GameObject explosion;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            explosion.SetActive(true);
        }
    }
}
