using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : ShootingEnemy
{
    public Transform origin;
    public Vector2 boxSize;
    public LayerMask whoToShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    void CastRay()
    {
        RaycastHit2D hit = Physics2D.BoxCast(origin.position, boxSize, 0f, Vector2.left, whoToShoot);
        Debug.Log(hit.collider);
        if(hit.collider != null)
        {
            Shoot();
        }
    }
}
