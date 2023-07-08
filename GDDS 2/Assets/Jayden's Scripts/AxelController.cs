using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AxelController : PlayerController
{
    Animator myAnim;

    [Header("EMP")]
    public float empCooldown = 30f;
    float currentEMPCooldown;
    public float empDuration = 5f;
    public GameObject empEffect;
    [SerializeField] Image empImageCooldown;
    [SerializeField] TMP_Text textCooldown;

    [Header("Deflect")]
    public float deflectCooldown = 30f;
    float currentDeflectCooldown;
    public float deflectDuration = 5f;
    public GameObject deflectEffect;
    [SerializeField] Image deflectImageCooldown;
    [SerializeField] TMP_Text deflectText;

    [Header("LilBoy")]
    public bool isJumping;
    public float jumpHigher;
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    [SerializeField] bool isHovering;
    float hoveringCounter;
    [SerializeField] bool hoveringCooldown;
    float hoverCooldown = 2.5f;
    float hoverDuration = 3f;
    [SerializeField] bool canInput;
    // Start is called before the first frame update
    void Start()
    {
        landButton.GetComponent<Button>().onClick.AddListener(() => EMPActivated());
        spaceButton.GetComponent<Button>().onClick.AddListener(() => DeflectBullets());
        currentEMPCooldown = empCooldown;
        currentDeflectCooldown = deflectCooldown;
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.GetInt("PowerupForAxel") == 0) {
            empCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("PowerupForAxel") == 1) {
            empCooldown = 25;
        }
        else if (PlayerPrefs.GetInt("PowerupForAxel") == 2) {
            empCooldown = 20;
        }
        else if (PlayerPrefs.GetInt("PowerupForAxel") == 3) {
            empCooldown = 15;
        }


        if (PlayerPrefs.GetInt("SkillForAxel") == 0) {
            empCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("SkillForAxel") == 1) {
            deflectCooldown = 25;
        }
        else if (PlayerPrefs.GetInt("SkillForAxel") == 2) {
            deflectCooldown = 20;
        }
        else if (PlayerPrefs.GetInt("SkillForAxel") == 3) {
            deflectCooldown = 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetBool("OnLand", onLand);
        myAnim.SetBool("InSpace", isInSpace);
        myAnim.SetBool("isGrounded", isGrounded);
        if (onLand)
        {
            GroundCheck();
            EMPCooldown();
            GroundBehaviour();
        }
        else if (isInSpace)
        {
            Fire(weaponDamage);
            DeflectCooldown();
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
                        if (!canInput) return;
                        switch (t.phase)
                        {
                            case TouchPhase.Began:
                                print("Began Touch " + i);
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                touchTimer += Time.deltaTime;
                                if (isGrounded)
                                {
                                    touchTimer += Time.deltaTime;
                                    LandBehaviour();
                                }
                                else if (touchTimer > 0.3f)
                                {
                                    if (hoveringCooldown) return;
                                    isHovering = true;
                                    HoldBehaviour();
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
                                touchTimer = 0f;
                                isJumping = true;
                                LandBehaviour();
                                isHovering = false;
                                hoveringCounter = 0f;
                                rb.gravityScale = 1f;
                                jumpTime = 0f;
                                break;
                            case TouchPhase.Canceled:
                                print("Cancelled Touch " + i);
                                LandBehaviour();
                                break;
                        }

                    }
                    else if (isInSpace)
                    {
                        Fire(weaponDamage);
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
                    if (!canInput) return;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        touchTimer += Time.deltaTime;
                        if (isGrounded)
                        {
                            touchTimer += Time.deltaTime;
                            LandBehaviour();
                        }
                        else if(touchTimer > 0.3f)
                        {
                             if (hoveringCooldown) return;
                             isHovering = true;
                             HoldBehaviour();
                        }
                        else 
                        {
                            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                        }
                    }
                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        touchTimer = 0f;
                        isJumping = true;
                        LandBehaviour();
                        isHovering = false;
                        hoveringCounter = 0f;
                        rb.gravityScale = 1f;
                        jumpTime = 0f;
                    }
                    GroundBehaviour();
                    
                }
                else if (isInSpace)
                {
                    SpaceBehaviour();
                }
            }
        }
        else
        {   
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    public override void LandBehaviour()
    {
        jumpTime += Time.deltaTime;

        if (isHovering) return;
        if (hoveringCooldown) return;
        if (isJumping == true)
        {
            rb.velocity = new Vector3(runSpeed, jumpForce, 0f);
            
        }
    }

    public void GroundBehaviour()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            isJumping = false;
            if (hoveringCooldown)
            {
                canInput = true;
                touchTimer = 0f;
                hoverCooldown -= Time.deltaTime;
                if (hoverCooldown <= 0)
                {
                    hoverCooldown = 2.5f;
                    hoveringCooldown = false;
                }
            }
        }
    }

    void HoldBehaviour()
    {
        if (hoveringCooldown) return;
        if (isHovering)
        {
            if (hoveringCounter < hoverDuration)
            {
                hoveringCounter += Time.deltaTime;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(runSpeed, 0f);
            }
            else
            {
                isHovering = false;
                hoveringCounter = 0f;
                hoveringCooldown = true;
                canInput = false;
                rb.gravityScale = 1f;
            }
        }
    }

    void EMPCooldown()
    {
        if(currentEMPCooldown > empCooldown)
        {
            textCooldown.text = "";
            empImageCooldown.fillAmount = 1f;
        }
        else
        {
            currentEMPCooldown += Time.deltaTime;
            textCooldown.text = Mathf.RoundToInt(empCooldown - currentEMPCooldown).ToString();
            empImageCooldown.fillAmount = currentEMPCooldown / empCooldown;
        }
    }

    void DeflectCooldown()
    {
        if (currentDeflectCooldown > deflectCooldown)
        {
            deflectText.text = "";
            deflectImageCooldown.fillAmount = 1f;
        }
        else
        {
            currentDeflectCooldown += Time.deltaTime;
            deflectText.text = Mathf.RoundToInt(deflectCooldown - currentDeflectCooldown).ToString();
            deflectImageCooldown.fillAmount = currentDeflectCooldown / deflectCooldown;
        }
    }

    public void EMPActivated() 
    {
        if (currentEMPCooldown < empCooldown) return;
        GameObject go = Instantiate(empEffect, transform.position, Quaternion.identity);
        currentEMPCooldown = 0f;
    }

    public void DeflectBullets()
    {
        Debug.Log("work?");
        //Cooldown
        if (currentDeflectCooldown < deflectCooldown) return;
        //Create Field that deflects 
        GameObject deflectShield = Instantiate(deflectEffect, transform.position, Quaternion.identity, this.transform);
        Destroy(deflectShield, deflectDuration);
        currentDeflectCooldown = 0f;
    }
}
