using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float timeSpawned;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawned += Time.deltaTime;
        if(timeSpawned > 5f)
        {
            Destroy(gameObject);
        }
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
}
