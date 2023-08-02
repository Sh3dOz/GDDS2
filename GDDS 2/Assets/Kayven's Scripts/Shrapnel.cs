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
        sharpnel1.tag = "Player";
        GameObject sharpnel2= Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -15));
        sharpnel2.GetComponent<Bullet>().damage = damage / 3;
        sharpnel2.tag = "Player";
        GameObject sharpnel3 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 15));
        sharpnel3.GetComponent<Bullet>().damage = damage / 3;
        sharpnel3.tag = "Player";
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
            if (collision.GetComponent<BossController>())
            {
                collision.GetComponent<BossController>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                collision.GetComponent<ShootingEnemy>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
