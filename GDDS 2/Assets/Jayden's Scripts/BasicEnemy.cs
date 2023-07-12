using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : ShootingEnemy
{
    public Transform origin;
    public Vector2 boxSize;
    public LayerMask whoToShoot;
    public bool isActivated;
    public GameObject expolsionEffect;
    // Start is called before the first frame update
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        if (manager)
        {
            if (manager.isWin == true) isActivated = false;
        }
        else
        {
            manager = FindObjectOfType<LevelManager>();
        }
        if (isActivated)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if(collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }
}
