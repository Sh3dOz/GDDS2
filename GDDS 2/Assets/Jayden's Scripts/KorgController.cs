using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Terresquall;

public class KorgController : PlayerController
{
    bool onUI;
    [Header("Gravity")]
    public float gravScale;
    bool accelerate = false;
    float gravRate = 0.2f;
    float maxGrav;
    float tempGravRate;
    bool flipped;
    bool unflip;
    bool flip;
    Animator myAnim;
    public GameObject arrowEffect;
    public GameObject arrowEffectFlip;
    public GameObject arrowPos;
    public AudioClip flipSound;

    [Header("Land Shield")]
    public float shieldCooldown = 30f;
    public float currentShieldCooldown;
    public float shieldDuration = 5f;
    public GameObject shieldPrefab;
    [SerializeField] bool isCooldown;
    [SerializeField] Image shieldImageCooldown;
    [SerializeField] TMP_Text textCooldown;

    [Header("Hover")]
    [SerializeField] float hoverGrav;
    [SerializeField] bool hovering;
    [SerializeField] bool isHovering;
    float hoveringCounter;
    [SerializeField] bool hoveringCooldown;
    float hoverCooldown = 2.5f;
    float hoverDuration = 3f;
    float tempGrav;
    bool tempFlip;
    bool checkFlip;
    public GameObject hoverPrefab, hoverParticle;
    int flips;

    [Header("Space Missile")]
    [SerializeField] float missileCooldown = 30f;
    [SerializeField] float currentMissileCooldown;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] Image missileImageCooldown;
    [SerializeField] TMP_Text missileText;
    // Start is called before the first frame update
    void Start()
    {
        landButton.GetComponent<Button>().onClick.AddListener(() => ShieldActive());
        spaceButton.GetComponent<Button>().onClick.AddListener(() => ShootMissile());
        myAnim = GetComponent<Animator>();
        currentShieldCooldown = shieldCooldown;
        currentMissileCooldown = missileCooldown;
        maxGrav = gravScale;
        tempGravRate = gravRate;
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
        if (PlayerPrefs.GetInt("KorgPowerup") == 0) {
            missileCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("KorgPowerup") == 1) {
            missileCooldown = 27;
        }
        else if (PlayerPrefs.GetInt("KorgPowerup") == 2) {
            missileCooldown = 24;
        }
        else if (PlayerPrefs.GetInt("KorgPowerup") == 3) {
            missileCooldown = 21;
        }


        if (PlayerPrefs.GetInt("KorgSkill") == 0) {
            shieldCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("KorgSkill") == 1) {
            shieldCooldown = 27;
        }
        else if (PlayerPrefs.GetInt("KorgSkill") == 2) {
            shieldCooldown = 24;
        }
        else if (PlayerPrefs.GetInt("KorgSkill") == 3) {
            shieldCooldown = 21;
        }
        health = maxHealth;
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
            FlipCheck();
            GroundCheck();
            ShieldCooldown();
            GroundBehaviour();
        }
        else if (isInSpace)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
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
                        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                        {
                            Debug.Log("touched UI");
                            onUI = true;
                            return;
                        }
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                print("Began Touch " + i);
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                if (isGrounded) return;
                                touchTimer += Time.deltaTime;
                                if (touchTimer > 0.2f && !hoveringCooldown)
                                {
                                    isHovering = true;
                                    if (hovering == false) hoverGrav = gravScale;
                                    hovering = true;
                                    HoldBehaviour();
                                }
                                else if (touchTimer > 0.1f && hoveringCooldown && flips < 1)
                                {
                                    LandBehaviour();
                                    flips++;
                                }
                                else
                                {
                                    rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                                }
                                break;
                            case TouchPhase.Moved:
                                print("Moving Touch " + i);
                                break;
                            case TouchPhase.Ended:
                                print("Ended Touch " + i);
                                if (onUI)
                                {
                                    onUI = false;
                                }
                                else
                                {
                                    if (!isHovering && flips < 1)
                                    {
                                        LandBehaviour();
                                    }
                                    else if (isHovering)
                                    {
                                        accelerate = false;
                                        StartCoroutine("GravWait");
                                    }
                                    touchTimer = 0f;
                                    isHovering = false;
                                    flips = 0;
                                    hoveringCounter = 0f;
                                    Destroy(hoverParticle);
                                }
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
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                SpaceBehaviour();
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
                    manager.SwitchMode();
                    if (isInSpace) transform.localScale = new Vector3(1f, 1f, 1f);
                }
                if (onLand)
                {
                    GroundBehaviour();
                    if (Input.GetKey(KeyCode.Space))
                    {
                        if (isGrounded) return;
                        touchTimer += Time.deltaTime;
                        if (touchTimer > 0.1f)
                        {
                            if (hoveringCooldown) return;
                            isHovering = true;
                            if (hovering == false) hoverGrav = gravScale;
                            hovering = true;
                            HoldBehaviour();
                        }
                        else
                        {
                            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                        }
                    }

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        if (!isHovering)
                        {
                            if (hoverParticle)
                            {
                                Destroy(hoverParticle);
                            }
                            LandBehaviour();
                        }
                        else
                        {
                            accelerate = false;
                            StartCoroutine("GravWait");
                        }
                        touchTimer = 0f;
                        isHovering = false;
                        hoveringCounter = 0f;
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
        if (hovering)
        {
            accelerate = false;
            StartCoroutine("GravWait");
            return;
        }
        else if (flipped)
        {
            StopCoroutine("GravWait");
            hovering = false;
            checkFlip = false;
            UI.PlayOneShot(flipSound);
            Instantiate(arrowEffectFlip, arrowPos.transform.position, Quaternion.identity);
            accelerate = true;
            unflip = true;
            StartCoroutine("GravWait");
        }
        else
        {
            StopCoroutine("GravWait");
            checkFlip = false;
            hovering = false;
            UI.PlayOneShot(flipSound);
            Instantiate(arrowEffect, arrowPos.transform.position, Quaternion.identity);
            accelerate = true;
            StartCoroutine("GravWait");
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
            if (hoveringCooldown)
            {
                touchTimer = 0f;
                hoverCooldown -= Time.deltaTime;
                if (hoverCooldown <= 0)
                {
                    hoverCooldown = 2.5f;
                    hoveringCooldown = false;
                }
            }
        }
        else
        {
            rb.velocity = new Vector2(runSpeed, gravScale);
        }
    }

    void FlipCheck()
    {
        if(flip && unflip)
        {
            flip = false;
            unflip = false;
        }
        FlipRun();
    }
    IEnumerator GravWait()
    {
        if (accelerate)
        {
            if (!checkFlip)
            {
                if (flipped)
                {
                    Debug.Log("Flip");
                    maxGrav = -12f;
                    tempGrav = -5f;
                    tempFlip = true;
                }
                else
                {
                    Debug.Log("Unflip");
                    maxGrav = 12f;
                    tempGrav = 5f;
                    tempFlip = false;
                }
                checkFlip = true;
            }
            while (gravScale != maxGrav)
            {
                gravScale = tempGrav;
                if (tempFlip)
                {
                    gravScale -= gravRate;
                }
                else
                {
                    gravScale += gravRate;
                }
                gravRate += gravRate;
                if (Mathf.Abs(gravScale) > Mathf.Abs(maxGrav))
                {
                    gravScale = maxGrav;
                    tempFlip = !tempFlip;
                    checkFlip = false;
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
            Debug.Log("Hover");
            while (gravScale != maxGrav)
            {
                gravScale = hoverGrav;
                if (tempFlip)
                {
                    gravScale -= gravRate;
                }
                else
                {
                    gravScale += gravRate;
                }
                gravRate += gravRate;
                if (Mathf.Abs(gravScale) > Mathf.Abs(maxGrav))
                {
                    gravScale = maxGrav;
                }
                yield return new WaitForSeconds(.1f);
            }
            hovering = false;
            gravRate = tempGravRate;
        }
    }

    void HoldBehaviour()
    {
        if (hoveringCooldown) return;
        if (hoverParticle == null)
        {
            hoverParticle = Instantiate(hoverPrefab, groundCheck.position, Quaternion.Euler(85f,0f,0f), this.gameObject.transform);
            if (flipped)
            {
                //hoverParticle.transform.localScale = new Vector3(1f, -1, 1f);
                var hoverSettings = hoverParticle.GetComponent<ParticleSystem>().velocityOverLifetime; 
                hoverSettings.xMultiplier = 40;
            }
        }
        if (isHovering)
        {
            if (hoveringCounter < hoverDuration)
            {
                hoveringCounter += Time.deltaTime;
                gravScale = 0f;
                rb.velocity = new Vector2(runSpeed, gravScale);
            }
            else
            {
                isHovering = false;
                hoveringCounter = 0f;
                hoveringCooldown = true;
                touchTimer = 0f;
                Destroy(hoverParticle);
                LandBehaviour();

            }
        }
        else
        {
            hovering = false;
        }
    }

    public void Unflip()
    {
        if (unflip)
        {
            unflip = false;
        }
    }
    public void Flip()
    {
        if (flip)
        {
            flip = false;
        }
    }

    public void FlipRun()
    {
        if (flipped)
        {
            transform.localScale = new Vector2(transform.localScale.x, -1f);
        }
        else
        {
            transform.localScale = new Vector2(transform.localScale.x, 1f);
        }
    }

    public void ShieldCooldown()
    {
        if (currentShieldCooldown >= shieldCooldown)
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
        if (currentMissileCooldown >= missileCooldown)
        {
            missileText.text = "";
            missileImageCooldown.fillAmount = 1f;
        }
        else
        {
            currentMissileCooldown += Time.deltaTime;
            missileText.text = Mathf.RoundToInt(missileCooldown - currentMissileCooldown).ToString();
            missileImageCooldown.fillAmount = currentMissileCooldown / missileCooldown;
        }
    }
    public void ShootMissile()
    {
        if (currentMissileCooldown < missileCooldown) return;
        Instantiate(missilePrefab, shootPos.position, Quaternion.identity);
        currentMissileCooldown = 0f;
    }
}

