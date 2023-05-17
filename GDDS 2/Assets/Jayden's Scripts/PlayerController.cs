using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public bool isInSpace;
    public bool onLand;
    public float moveSpeed;
    public float runSpeed;
    public float jumpHeight;
    public Vector2 movement;
    public Rigidbody2D rb;
    public List<Weapon> weapons;
    public int currentWeapon = 0;
    public GameObject joystick;

    [Header("Gravity")]
    public float gravScale;
    bool accelerate = false;
    float gravRate = 0.2f;
    float tempGrav;
    float tempGravRate;
    bool flipped;

    [Header("Health")]
    public int health = 3;
    public bool isDamaged;
    public Collider2D col;
    LevelManager manager;

    [Header("GroundCheck")]
    public bool isGrounded;
    public LayerMask groundLayer;
    public float cirlceRadius;
    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        tempGrav = gravScale;
        tempGravRate = gravRate;
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cirlceRadius, groundLayer);
        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);

                if (onLand)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            print("Began Touch " + i);
                            if (flipped)
                            {
                                accelerate = true; StartCoroutine(GravWait());
                            }
                            else
                            {
                                gravScale = -gravScale;
                            }
                            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                            flipped = !flipped;

                            if (isGrounded)
                            {
                                rb.velocity = new Vector2(runSpeed, 0f);
                            }
                            else
                            {
                                rb.velocity = new Vector2(runSpeed, gravScale);
                            }
                            break;
                        case TouchPhase.Stationary:
                            print("Stationary Touch " + i);
                            break;
                        case TouchPhase.Moved:
                            print("Moving Touch " + i);
                            break;
                        case TouchPhase.Ended:
                            print("Ended Touch " + i);
                            break;
                        case TouchPhase.Canceled:
                            print("Cancelled Touch " + i);
                            break;
                    }
                }
                else if(isInSpace)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            print("Began Touch " + i);
                            joystick.transform.position = t.position;
                            break;
                        case TouchPhase.Stationary:
                            print("Stationary Touch " + i);
                            break;
                        case TouchPhase.Moved:
                            print("Moving Touch " + i);
                            movement.x = VirtualJoystick.GetAxis("Horizontal", 0);
                            movement.y = VirtualJoystick.GetAxis("Vertical", 0);
                            Movement();
                            Fire();
                            break;
                        case TouchPhase.Ended:
                            print("Ended Touch " + i);
                            break;
                        case TouchPhase.Canceled:
                            print("Cancelled Touch " + i);
                            break;
                    }
                }
            }
        }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, cirlceRadius, groundLayer);
        if (isInSpace)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Movement();
            Fire();
        }
        else if(onLand)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (flipped)
                {
                    accelerate = true; StartCoroutine(GravWait());
                }
                else
                {
                    gravScale = -gravScale;
                }
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                flipped = !flipped;
            }

            if (isGrounded)
            {
                rb.velocity = new Vector2(runSpeed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(runSpeed, gravScale);
            }
        }
    }

    public void Movement()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        if (isDamaged != true) health -= damage; manager.healthSlider.value = health;
        if (health <= 0)
        {
            manager.isAlive = true;
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

    IEnumerator GravWait()
    {
        if (accelerate)
        {
            while (gravScale != tempGrav)
            {
                Debug.Log("accelerate");
                gravScale = -5f;
                gravScale -= gravRate;
                gravRate += gravRate;
                if (Mathf.Abs(gravScale) > Mathf.Abs(tempGrav))
                {
                    gravScale = tempGrav;
                }
                yield return new WaitForSeconds(.1f);
            }
            accelerate = false;
            gravRate = tempGravRate;
        }
    }

    void Fire()
    {
        weapons[currentWeapon].Fire();
    }
}

