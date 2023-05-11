using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public List<SpaceEnemy> currentEnemies;

    public float turnSpeed = 40f;
    public Transform target;
    public float lifespan = 2;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a target, we home in on the target.
        if (target)
        {
            Vector3 desiredFacing = target.position - transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(desiredFacing);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
        }

        rb.velocity = transform.forward * speed;
    }

    public override void DestroyBullet()
    {
        
    }
}
