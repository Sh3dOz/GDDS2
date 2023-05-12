using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrapnel : Bullet {

    public GameObject bullet; // For split.

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        DestroyBullet();
    }

    void Split() {
        GameObject sharpnel1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 0));
        sharpnel1.GetComponent<Bullet>().damage = damage / 3;
        GameObject sharpnel2= Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -45));
        sharpnel2.GetComponent<Bullet>().damage = damage / 3;
        GameObject sharpnel3 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 45));
        sharpnel3.GetComponent<Bullet>().damage = damage / 3;
    }

    public override void DestroyBullet()
    {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > .68f)
        {
            Split();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ShootingEnemy>())
        {
            Split();
        }
    }
}
