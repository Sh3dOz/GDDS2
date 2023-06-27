using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Terresquall;

public class KorgController : PlayerController
{
    [Header("Gravity")]
    public float gravScale;
    bool accelerate = false;
    float gravRate = 0.2f;
    float tempGrav;
    float tempGravRate;
    float hoverGrav;
    bool hovering;
    bool flipped;
    // Start is called before the first frame update
    void Start()
    {
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
        GroundCheck();
        ShieldCooldown();
        if (manager.isWin) canMove = false;
        if (!manager.isWin) canMove = true;
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
                                if (t.position.x < Screen.width / 2)
                                {
                                    LandBehaviour();
                                }
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                StopCoroutine("GravWait");
                                hovering = true;
                                hoverGrav = gravScale;
                                gravScale = 0f;
                                break;
                            case TouchPhase.Moved:
                                print("Moving Touch " + i);
                                break;
                            case TouchPhase.Ended:
                                print("Ended Touch " + i);
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
            }
        }
        else
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    public override void LandBehaviour()
    {
        if (flipped)
        {
            accelerate = true; 
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
        }
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
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
}

