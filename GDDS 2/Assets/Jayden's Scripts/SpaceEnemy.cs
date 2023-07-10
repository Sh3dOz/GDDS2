using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemy : ShootingEnemy
{
    public float moveSpeed;
    Rigidbody2D rb;
    public GameObject target;
    public Transform targetPos;
    public bool inCombat;
    public bool isBoss;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.Find("Sound Audio").GetComponent<AudioSource>();
        bulletCount = maxBullet;
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoss) return;
        Shoot(); Movement();
    }

    public void Movement()
    {
        if(!inCombat) transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPos.position.x, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPos.position.y, 1f * Time.deltaTime));
        if (transform.position.x == targetPos.position.x) inCombat = true;
        if (inCombat)
        {
            rb.velocity = new Vector2(moveSpeed, 0f);
            if (target != null)
            {
                transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, target.transform.position.y, 1f * Time.deltaTime));
            }
        }
    }
}
