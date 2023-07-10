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
        tempGrav = gravScale;
        tempGravRate = gravRate;
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt("PowerupForKorg") == 0) {
            shieldCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("PowerupForKorg") == 1) {
            shieldCooldown = 25;
        }
        else if (PlayerPrefs.GetInt("PowerupForKorg") == 2) {
            shieldCooldown = 20;
        }
        else if (PlayerPrefs.GetInt("PowerupForKorg") == 3) {
            shieldCooldown = 15;
        }


        if (PlayerPrefs.GetInt("SkillForKorg") == 0) {
            missileCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("SkillForKorg") == 1) {
            missileCooldown = 25;
        }
        else if (PlayerPrefs.GetInt("SkillForKorg") == 2) {
            missileCooldown = 20;
        }
        else if (PlayerPrefs.GetInt("SkillForKorg") == 3) {
            missileCooldown = 15;
        }
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
                                else
                                {
                                    Debug.Log("touch UI");
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
                    manager.SwitchMode();
                    if(isInSpace) transform.localScale = new Vector3(1f, 1f, 1f);
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
            UI.PlayOneShot(flipSound);
            Instantiate(arrowEffectFlip, arrowPos.transform.position, Quaternion.identity);
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
            UI.PlayOneShot(flipSound);
            Instantiate(arrowEffect, arrowPos.transform.position, Quaternion.identity);
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
        }
        else
        {
            rb.velocity = new Vector2(runSpeed, gravScale);
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

