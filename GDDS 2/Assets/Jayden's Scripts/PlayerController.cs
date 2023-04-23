using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isInSpace;
    public float moveSpeed;
    public float runSpeed;
    public float jumpHeight;
    public Vector2 movement;
    public Rigidbody2D rb;
    public float gravScale;
    public GameObject bulletPrefab;
    public Transform shootPos;
    public float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInSpace)
        {
            rb.gravityScale = 0;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Movement();
        }
        else
        {
            rb.gravityScale = gravScale;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space Pressed");
                gravScale = -gravScale;
            }
            rb.velocity = new Vector2(runSpeed, movement.y * jumpHeight);
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
            }
            
        }
    }

    public void Movement()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }
}
