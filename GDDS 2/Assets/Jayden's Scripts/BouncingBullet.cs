using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : Bullet
{

    Vector2 lastReflection;
    Vector2 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
        Movement();
        lastVelocity = rb.velocity;
    }

    public override void DestroyBullet()
    {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<CameraCollider>())
        {
            if (collision.gameObject.GetComponent<CameraCollider>().isBullet)
            {
                lastReflection = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
                transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(lastReflection.y, lastReflection.x) * Mathf.Rad2Deg);
                rb.velocity = Vector2.zero;
                rb.MovePosition(rb.position + lastReflection * Time.fixedDeltaTime);
            }
        }
        if (this.tag == "Enemy")
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                if (collision.gameObject.GetComponent<PlayerController>().isDamaged || !collision.gameObject.GetComponent<PlayerController>().canBeDamaged)
                {
                    collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
