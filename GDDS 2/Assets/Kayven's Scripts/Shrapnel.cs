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
        if (timeSpawned > 2f) {
            Split();
            Destroy(gameObject);
        }
    }

    void Split() {
        GameObject sharpnel1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        sharpnel1.GetComponent<Bullet>().damage = damage / 3;
        GameObject sharpnel2= Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -22));
        sharpnel2.GetComponent<Bullet>().damage = damage / 3;
        GameObject sharpnel3 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 22));
        sharpnel3.GetComponent<Bullet>().damage = damage / 3;
    }
}
