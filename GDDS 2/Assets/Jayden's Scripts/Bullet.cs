using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public float timeSpawned;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (this.tag == "Player")
        {
            Debug.Log("haro?");
            Debug.Log(collision.gameObject);
            if (collision.GetComponent<BossController>())
            {
                collision.GetComponent<BossController>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (collision.GetComponent<SpaceEnemy>())
            {
                collision.GetComponent<SpaceEnemy>().TakeDamage(damage, this.gameObject);
                Destroy(gameObject);
            }
        }
        if (this.tag == "Enemy")
        {
            if (collision.GetComponent<PlayerController>())
            {
                Debug.Log("haro?");
                if (collision.GetComponent<PlayerController>().isDamaged || !collision.GetComponent<PlayerController>().canBeDamaged)
                {
                    Debug.Log("damaged");
                }
                else
                {
                    collision.GetComponent<PlayerController>().TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
        if (collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }

    public abstract void DestroyBullet();

    public void Movement()
    {
        rb.velocity = transform.right * speed;
    }
}
