using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Terresquall;

public class XavierController : PlayerController {

    public float gravScale;
    Animator myAnim;

    [Header("Land Shield")]
    public float shieldCooldown = 30f;
    public float currentShieldCooldown;
    public float shieldDuration = 5f;
    public GameObject shieldPrefab;
    [SerializeField] bool isCooldown;
    [SerializeField] Image shieldImageCooldown;
    [SerializeField] TMP_Text textCooldown;

    public Transform top;
    public Transform bottom;
    public bool outOfBounds = false;


    void Start() {
        myAnim = GetComponent<Animator>();
        currentShieldCooldown = shieldCooldown;
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update() {
        if (onLand) {
            GroundCheck();
            GroundBehaviour();
        }
        else if (isInSpace) {
            Fire(weaponDamage);
        }

        if (manager.isWin) canMove = false;
        if (canMove) {
            if (Input.touchCount > 0) {

                for (int i = 0; i < Input.touchCount; i++) {
                    Touch t = Input.GetTouch(i);
                    if (onLand) {
                        switch (t.phase) {
                            case TouchPhase.Began:
                                print("Began Touch " + i);
                                break;
                            case TouchPhase.Stationary:
                                print("Stationary Touch " + i);
                                //StopCoroutine("GravWait");
                                //hovering = true;
                                break;
                            case TouchPhase.Moved:
                                //MouseDetect();
                                LandBehaviour();
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
                    else if (isInSpace) {
                        switch (t.phase) {
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
            else {
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    ToggleMode();
                }
                if (onLand) {
                    GroundBehaviour();
                    if (Input.GetKeyDown(KeyCode.Space)) {
                        LandBehaviour();
                    }
                }
                else if (isInSpace) {
                    SpaceBehaviour();

                }

                else {
                    rb.velocity = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }


    public void MouseDetect() {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float newYPosition = touchPosition.y;

        if (touchPosition.y < top.position.y || touchPosition.y > bottom.position.y) {
            LandBehaviour();
        }
    }
    public override void LandBehaviour() {

 
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float newYPosition = touchPosition.y;

                Vector3 newPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
                transform.position = newPosition;
        }
    public void GroundBehaviour() {
        rb.velocity = new Vector2(runSpeed, 0f);
        Debug.Log("haro?");
    }
}
