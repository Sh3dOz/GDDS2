using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Terresquall;

public class KorgController : PlayerController
{
    [Header("Gravity")]
    public float gravScale;
    bool accelerate = false;
    float gravRate = 0.2f;
    float tempGrav;
    float tempGravRate;
    [SerializeField] float hoverGrav;
    [SerializeField] bool hovering;
    bool flipped;
    bool unflip;
    bool flip;
    Animator myAnim;

    [Header("Land Shield")]
    public float shieldCooldown = 30f;
    public float currentShieldCooldown;
    public float shieldDuration = 5f;
    public GameObject shieldPrefab;
    [SerializeField] bool isCooldown;
    [SerializeField] Image shieldImageCooldown;
    [SerializeField] TMP_Text textCooldown;

    [Header("Space Missile")]
    [SerializeField] float missileCooldown = 30f;
    float currentMissileCooldown;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject missilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        currentShieldCooldown = shieldCooldown;
        tempGrav = gravScale;
        tempGravRate = gravRate;
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetBool("Flip", flip);
        myAnim.SetBool("Unflip", unflip);
        myAnim.SetBool("OnLand", onLand);
        myAnim.SetBool("InSpace", isInSpace);
        if (onLand)
        {
            GroundCheck();
            ShieldCooldown();
            GroundBehaviour();
        }
        else if (isInSpace)
        {
            Fire(weaponDamage);
            MissileCooldown();
        }
        if (manager.isWin) canMove = false;
        if (canMove)
        {
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
                                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                                {
                                    LandBehaviour();
                                }
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                //StopCoroutine("GravWait");
                                //hovering = true;
                                break;
                            case TouchPhase.Moved:
                                print("Moving Touch " + i);
                                break;
                            case TouchPhase.Ended:
                                print("Ended Touch " + i);
                                break;
                            case TouchPhase.Canceled:
                                print("Cancelled Touch " + i);
                                if (hovering)
                                {
                                    LandBehaviour();
                                }
                                break;
                        }
                    }
                    else if (isInSpace)
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
                                SpaceBehaviour();
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
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    ToggleMode();
                }
                if (onLand)
                {
                    GroundBehaviour();
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        LandBehaviour();
                    }
                }
                else if (isInSpace)
                {
                    SpaceBehaviour();

                }

                else
                {
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }

    

    public override void LandBehaviour()
    {
        if (flipped)
        {
            accelerate = true;
            unflip = true;
            StartCoroutine("GravWait");
        }
        else if (hovering)
        {
            StartCoroutine("GravWait");
            return;
        }
        else
        {
            StopCoroutine("GravWait");
            accelerate = false;
            gravScale = -gravScale;
            flip = true;
        }
        flipped = !flipped;
    }

    public void GroundBehaviour()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(runSpeed, 0f);
            Debug.Log("haro?");
        }
        else
        {
            rb.velocity = new Vector2(runSpeed, gravScale);
            Debug.Log("ground?");
        }
    }

    IEnumerator GravWait()
    {
        if (accelerate)
        {
            while (gravScale != tempGrav)
            {
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
            Unflip();
            Flip();
        }
        else if (hovering)
        {
            gravScale = hoverGrav;
            while (gravScale != tempGrav)
            {
                gravScale = -5f;
                gravScale -= gravRate;
                gravRate += gravRate;
                if (Mathf.Abs(gravScale) > Mathf.Abs(tempGrav))
                {
                    gravScale = tempGrav;
                }
                yield return new WaitForSeconds(.1f);
            }
            hovering = false;
            gravRate = tempGravRate;
        }
    }

    public void Unflip()
    {
        if (unflip)
        {
            unflip = false;
        }
        if(transform.localScale.y > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }
    }
    public void Flip()
    {
        if (flip)
        {
            flip = false;
        }
        if (transform.localScale.y < 0)
        {
            transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
        }
    }

    public void FlipSprite()
    {
        transform.localScale = new Vector2(transform.localScale.x, -transform.localScale.y);
    }

    public void ShieldCooldown()
    {
        if (currentShieldCooldown > shieldCooldown)
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
        canBeDamaged = false;
        Instantiate(shieldPrefab, transform.position, Quaternion.identity, this.gameObject.transform);
        yield return new WaitForSeconds(shieldDuration);
        canBeDamaged = true;

        yield return new WaitForSeconds(shieldCooldown);//Cooldown
    }

    public void MissileCooldown()
    {
        if (currentMissileCooldown > missileCooldown)
        {
            textCooldown.text = "";
            shieldImageCooldown.fillAmount = 1f;
        }
        else
        {
            currentShieldCooldown += Time.deltaTime;
            textCooldown.text = Mathf.RoundToInt(missileCooldown - currentMissileCooldown).ToString();
            shieldImageCooldown.fillAmount = currentMissileCooldown / missileCooldown;
        }
    }
    public void ShootMissile()
    {
        Instantiate(missilePrefab, shootPos.position, Quaternion.identity);
    }
}

