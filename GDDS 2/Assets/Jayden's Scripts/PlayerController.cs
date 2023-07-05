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
    public Sprite playerSprite;
    public bool canMove = true;
    public float touchTimer;

    [Header("Health")]
    public int health = 3;
    public bool isDamaged;
    public Collider2D col;
    public LevelManager manager;
    public bool canBeDamaged = true;

    [Header("GroundCheck")]
    public bool isGrounded;
    public LayerMask groundLayer;
    public float cirlceRadius;
    public Transform groundCheck;

    [Header("Transition")]
    public GameObject landButton;
    public GameObject spaceButton;
    

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

        if (isDamaged != true && canBeDamaged == true)
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
        if (canBeDamaged == false || isDamaged == false)
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



    public void ToggleMode()
    {
        if (onLand)
        {
            landButton.SetActive(false);
            spaceButton.SetActive(true);
            joystick.SetActive(true);
            sr.sprite = spaceShip;
            UpdateSprite();
            //playerSprite.SetActive(false);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (isInSpace)
        {
            landButton.SetActive(true);
            spaceButton.SetActive(false);
            joystick.SetActive(false);
            sr.sprite = playerSprite;
            UpdateSprite();
            //playerSprite.SetActive(true);
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
        //movement.x = VirtualJoystick.GetAxis("Horizontal", 0);
        //movement.y = VirtualJoystick.GetAxis("Vertical", 0);
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


