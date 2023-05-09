using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : Bullet {

    public GameObject bullet; // For split.

    // Start is called before the first frame update
    void Start() {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update() {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 5f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Shield>()) {
            Split();
            Destroy(gameObject);
        }
    }

    void Split() {
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -22));
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 22));
    }
}
