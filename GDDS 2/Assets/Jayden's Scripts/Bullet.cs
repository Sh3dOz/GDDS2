using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float timeSpawned;
    public int damage;

    public AudioSource audioS;
    public AudioClip shotSound;
    public GameObject shotEffect;


    // Start is called before the first frame update
    void Start()
    {
        audioS = FindObjectOfType<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.GetComponent<PlayerController>().canBeDamaged) {
                collision.GetComponent<PlayerController>().TakeDamage(damage);
                audioS.PlayOneShot(shotSound);
        }
            if (this.tag == "Player") {
                if (collision.GetComponent<BossController>()) {
                    collision.GetComponent<BossController>().TakeDamage(damage);
                    Destroy(gameObject);
                }
                else if (collision.GetComponent<ShootingEnemy>()) {
                    if (this.tag == "Enemy") return;
                    collision.GetComponent<ShootingEnemy>().TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
            if (this.tag == "Enemy") {
                if (collision.GetComponent<PlayerController>()) {
                    collision.GetComponent<PlayerController>().TakeDamage(damage);
                    if (collision.GetComponent<PlayerController>().isDamaged) return;
                    Destroy(gameObject);
                }
            }
        }

    public abstract void DestroyBullet();

    public void Movement()
    {
        rb.velocity = transform.right * speed;
    }
}
