using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxelController : PlayerController
{
    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        currentShieldCooldown = shieldCooldown;
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
                                if (t.position.x < Screen.width / 2)
                                {
                                    LandBehaviour();
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
        
    }

    public void GroundBehaviour()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(runSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(runSpeed, 0f);
        }
    }
}
