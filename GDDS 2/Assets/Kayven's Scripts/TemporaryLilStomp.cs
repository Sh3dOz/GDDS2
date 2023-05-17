using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryLilStomp : MonoBehaviour {
    [Header("Movement")]
    public bool isInSpace;
    public float moveSpeed;
    public float runSpeed;
    public float jumpHeight;
    public Vector2 movement;
    public Rigidbody2D rb;
    public List<Weapon> weapons;
    public int currentWeapon = 0;

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

    [Header("LilBoy")]
    public bool isRobot;
    public bool isJumping;    
    public float jumpHigher;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        tempGrav = gravScale;
        tempGravRate = gravRate;
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
    }

    // Update is called once per frame
    void Update() {


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cirlceRadius, groundLayer);

        if (isInSpace) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Movement();
            Fire();
        }
        else {

            if (isGrounded) {
                gravScale = -12;
                rb.velocity = new Vector2(runSpeed, 0f);
            }
            else {
                rb.velocity = new Vector2(runSpeed, gravScale);
            }


            if (isRobot) {
                if (isGrounded && Input.GetKeyDown(KeyCode.Space)) {
                    isJumping = true;
                    rb.velocity = new Vector2(runSpeed, jumpForce);
                    jumpTimeCounter = jumpTime;
                    Debug.Log("jump");
                }

                if (Input.GetKey(KeyCode.Space) && isJumping) {
                    if (jumpTimeCounter > 0) {
                        rb.velocity = new Vector2(runSpeed, jumpHigher);
                        jumpTimeCounter -= Time.deltaTime;
                        Debug.Log("Hold");
                    }
                    else {
                        isJumping = false;
                        gravScale = -8f;
                    }
                }

                if (Input.GetKeyUp(KeyCode.Space)) {
                    isJumping = false;
                    gravScale = -8f;
                    Debug.Log("Bye");
                }

            }
            else {

                if (Input.GetKeyDown(KeyCode.Space) && isRobot == false) {
                    if (flipped) {
                        accelerate = true; StartCoroutine(GravWait());
                    }
                    else {
                        gravScale = -gravScale;
                    }
                    transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                    flipped = !flipped;
                }

                if (isGrounded) {
                    rb.velocity = new Vector2(runSpeed, 0f);
                }
                else {
                    rb.velocity = new Vector2(runSpeed, gravScale);
                }
            }
        }
    
    }
    public void Movement() {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    public void TakeDamage(int damage) {
        if (isDamaged != true) health -= damage;
        if (health <= 0) {
            manager.isAlive = true;
            Destroy(gameObject);
        }
        isDamaged = true;
        StartCoroutine(WaitDamage());
    }

    IEnumerator WaitDamage() {
        Debug.Log("damaged");
        yield return new WaitForSeconds(1f);
        isDamaged = false;
    }

    IEnumerator GravWait() {
        if (accelerate) {
            while (gravScale != tempGrav) {
                Debug.Log("accelerate");
                gravScale = -5f;
                gravScale -= gravRate;
                gravRate += gravRate;
                if (Mathf.Abs(gravScale) > Mathf.Abs(tempGrav)) {
                    gravScale = tempGrav;
                }
                yield return new WaitForSeconds(.1f);
            }
            accelerate = false;
            gravRate = tempGravRate;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "TransformPower") {
            isRobot = true;
        }
    }

    void Fire() {
        weapons[currentWeapon].Fire();
    }
}