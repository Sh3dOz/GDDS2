using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : ShootingEnemy
{
    public float moveSpeed;
    Rigidbody2D rb;
    public Transform topPos;
    public Transform botPos;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        bulletCount = maxBullet;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot(); Movement();
    }

    public void Movement()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
        if (target != null)
        {
            transform.position = new Vector2(transform.position.x, Mathf.Lerp(transform.position.y, target.transform.position.y, 1f * Time.deltaTime));
        }
    }
}
