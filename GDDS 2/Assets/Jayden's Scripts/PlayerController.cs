using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;
using UnityEngine.UI;
using TMPro;

public abstract class PlayerController : MonoBehaviour
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
    public int weaponDamage;
    public GameObject joystick;
    public Sprite spaceShip;
    public SpriteRenderer sr;
    public GameObject playerSprite;
    public bool canMove = true;

    [Header("Health")]
    public int health = 3;
    public bool isDamaged;
    public Collider2D col;
    public LevelManager manager;

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
    public Sprite robotSprite; // Assign the robot sprite in the Inspector
     // Reference to the SpriteRenderer component
    public Sprite corgiSprite;

    [Header("Shield")]
    public bool isShielded;
    public float shieldCooldown = 30f;
    public float currentShieldCooldown;
    public float shieldDuration = 5f;
    public GameObject shieldButton;
    public GameObject shieldPrefab;
    [SerializeField] bool isCooldown;
    [SerializeField] Image shieldImageCooldown;
    [SerializeField] TMP_Text textCooldown;

    public bool isWin = false;
    public GameObject damagedEffect;
    public AudioSource UI;
    public AudioClip damageSound;

    public void Movement()
    {
        rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
    }

    public void TakeDamage(int damage)
    {

        if (isDamaged != true && isShielded == false)
        {
            health -= damage;
            manager.healthSlider.value = health;
            Instantiate(damagedEffect, transform.position, Quaternion.identity);
            UI.PlayOneShot(damageSound);
        }
        if (health <= 0)
        {
            manager.isAlive = false;
            Destroy(gameObject);
        }
        if (isShielded == false || isDamaged == false)
        {
            isDamaged = true;
        }
        StartCoroutine(waitDamage());
    }

    IEnumerator waitDamage()
    {
        Debug.Log("damaged");
        yield return new WaitForSeconds(1f);
        isDamaged = false;
    }


    public void Fire(int damage)
    {
        weapons[currentWeapon].damage = damage;
        weapons[currentWeapon].Fire();
    }

    public void ShieldActive()
    {
        StartCoroutine("ShieldEffect");
    }

    public IEnumerator ShieldEffect()
    {
        if (isCooldown) yield break;

        isCooldown = true;
        currentShieldCooldown = 0;
        shieldImageCooldown.fillAmount = 0.0f;
        isShielded = true;
        Instantiate(shieldPrefab, transform.position, Quaternion.identity, this.gameObject.transform);
        yield return new WaitForSeconds(shieldDuration);
        isShielded = false;

        yield return new WaitForSeconds(shieldCooldown);//Cooldown
    }

    public void ShieldCooldown()
    {
        if(currentShieldCooldown > shieldCooldown)
        {
            textCooldown.text = "";
            shieldImageCooldown.fillAmount = 1f;
            isCooldown = false;
        }
        else
        {
            currentShieldCooldown += Time.deltaTime;
            textCooldown.text = Mathf.RoundToInt(shieldCooldown - currentShieldCooldown).ToString();
            shieldImageCooldown.fillAmount = currentShieldCooldown / shieldCooldown;
        }
    }

    public void ToggleMode()
    {
        if (onLand)
        {
            shieldButton.SetActive(false);
            joystick.SetActive(true);
            sr.sprite = spaceShip;
            playerSprite.SetActive(false);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (isInSpace)
        {
            shieldButton.SetActive(true);
            joystick.SetActive(false);
            sr.sprite = null;
            playerSprite.SetActive(true);
        }
        onLand = !onLand;
        isInSpace = !isInSpace;
    }

    public void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, cirlceRadius, groundLayer);

    }

    public abstract void LandBehaviour();

    public void SpaceBehaviour()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.x = VirtualJoystick.GetAxis("Horizontal", 0);
        movement.y = VirtualJoystick.GetAxis("Vertical", 0);
        Movement();
    }

    void TryUpdateShapeToAttachedSprite(PolygonCollider2D collider)
    {
        UpdateShapeToSprite(collider,this.gameObject.GetComponent<SpriteRenderer>().sprite);
    }

    void UpdateShapeToSprite(PolygonCollider2D collider, Sprite sprite)
    {
        // ensure both valid
        if (collider != null && sprite != null)
        {
            // update count
            collider.pathCount = sprite.GetPhysicsShapeCount();

            // new paths variable
            List<Vector2> path = new List<Vector2>();

            // loop path count
            for (int i = 0; i < collider.pathCount; i++)
            {
                // clear
                path.Clear();
                // get shape
                sprite.GetPhysicsShape(i, path);
                // set path
                collider.SetPath(i, path.ToArray());
            }
        }
    }

    public void UpdateSprite() 
    {
        TryUpdateShapeToAttachedSprite(gameObject.GetComponent<PolygonCollider2D>());
    }
}


