using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : ShootingEnemy
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        if (hit.collider.gameObject.tag == "Level")
        {
            if (transform.position.y <= -2.45)
            {
                transform.position -= new Vector3(0f, moveSpeed, 0f);
            }
            else if (transform.position.y >= 3.95)
            {
                transform.position -= new Vector3(0f, -moveSpeed, 0f);
            }
        }
    }
}
