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
    public int damage;
    public int health = 3;
    public bool isDamaged;
    public Collider2D col;

    [Header("GroundCheck")]
    public bool isGrounded;
    public LayerMask groundLayer;
    public float cirlceRadius;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cirlceRadius, groundLayer);
        if (isInSpace)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Movement();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravScale = -gravScale;
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            if (isGrounded)
            {
                rb.velocity = new Vector2(runSpeed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(runSpeed, gravScale);
            }
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().damage = damage;
            }
            
        }
    }

    public void Movement()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        if (isDamaged != true) health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        isDamaged = true;
        StartCoroutine(waitDamage());
    }

    IEnumerator waitDamage()
    {
        Debug.Log("damaged");
        yield return new WaitForSeconds(1f);
        isDamaged = false;
    }
}
