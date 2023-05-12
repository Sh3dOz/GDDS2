using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float timeSpawned;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            if (collision.GetComponent<PlayerController>().isDamaged) return;
            Destroy(gameObject);
        }
        if (collision.GetComponent<ShootingEnemy>())
        {
            collision.GetComponent<ShootingEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public abstract void DestroyBullet();

    public void Movement()
    {
        rb.velocity = transform.right * speed;
    }
}
