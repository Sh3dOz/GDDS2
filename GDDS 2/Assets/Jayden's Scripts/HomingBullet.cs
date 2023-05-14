using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public List<SpaceEnemy> currentEnemies;

    public float turnSpeed = 180f;
    float dist = 100f;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If there is a target, we home in on the target.
        if (target)
        {
            Vector3 dir = target.position - transform.position;
            Vector3 desiredFacing =  Quaternion.Euler(0, 0, 90) * dir;
            Quaternion desiredRotation = Quaternion.LookRotation(Vector3.forward,desiredFacing);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            TargetCheck();
        }
        Movement();
        DestroyBullet();
    }

    public override void DestroyBullet()
    {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 1.5f)
        { 
            Destroy(gameObject);
        }
    }

    public void TargetCheck()
    {
        currentEnemies = new List<SpaceEnemy>(FindObjectsOfType<SpaceEnemy>());
        foreach( SpaceEnemy i  in currentEnemies)
        {
            Debug.Log(i.gameObject.transform.position);
            float tempDist = Vector3.Distance(i.gameObject.transform.position, transform.position);
            dist = tempDist;
            Debug.Log(tempDist);
            if (tempDist <= dist) dist = tempDist; target = i.gameObject.transform;
            Debug.Log(dist);
        }
    }
}
