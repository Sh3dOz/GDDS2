using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullet
{
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
    }

    public override void DestroyBullet()
    {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Vector3 rotationToAdd = new Vector3(0f,0f,(360f - transform.eulerAngles.z)*2);
            transform.Rotate(rotationToAdd);
        }
        if (this.tag == "Enemy")
        {
            if (collision.GetComponent<PlayerController>())
            {
                Debug.Log("haro?");
                if (collision.GetComponent<PlayerController>().isDamaged || !collision.GetComponent<PlayerController>().canBeDamaged)
                {
                    
                }
                else
                {
                    collision.GetComponent<PlayerController>().TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}
