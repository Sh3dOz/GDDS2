using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet

{

    public AudioSource audioS;
    public AudioClip shotSound;
    public GameObject shotEffect;
    public PlayerController[] players;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        players = FindObjectsOfType<PlayerController>();
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
        if (timeSpawned > 2f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponentInParent<PlayerController>()) {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            foreach (PlayerController player in players) {
                Instantiate(shotEffect, player.transform.position, Quaternion.identity);
            }
            audioS.PlayOneShot(shotSound);
            Debug.Log("ZAPped");
        }
        if (collision.tag == "EMP") {
            Destroy(this.gameObject);
        }
    }
}
