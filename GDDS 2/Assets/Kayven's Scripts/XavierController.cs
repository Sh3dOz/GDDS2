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

    [Header("Metorite")]
    public float metoriteCooldown = 30f;
    public float currentMetoriteCooldown;
    public GameObject metoritePrefab;
    int metoriteCharge;
    int maxCharge = 3;
    public float metorSpeed;
    float metorDuration = 10f;
    public Transform metorParent;
    public List<Transform> metoritePos = new List<Transform>();
    List<GameObject> metorites = new List<GameObject>();
    [SerializeField] Sprite selectMetor;
    [SerializeField] bool isCooldown;
    [SerializeField] Image metoriteImageCooldown;
    [SerializeField] TMP_Text textCooldown, amountCharge;

    public Transform top;
    public Transform bottom;

    void Start() {
        spaceButton.GetComponent<Button>().onClick.AddListener(() => LaunchMetorite());
        myAnim = GetComponent<Animator>();
        if (PlayerPrefs.GetInt("XPowerup") == 0)
        {
            metoriteCooldown = 30;
        }
        else if (PlayerPrefs.GetInt("XPowerup") == 1)
        {
            metoriteCooldown = 27;
        }
        else if (PlayerPrefs.GetInt("XPowerup") == 2)
        {
            metoriteCooldown = 24;
        }
        else if (PlayerPrefs.GetInt("XPowerup") == 3)
        {
            metoriteCooldown = 21;
        }
        currentMetoriteCooldown = metoriteCooldown;
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
        weapons = new List<Weapon>(GetComponentsInChildren<Weapon>(true));
        // Get the SpriteRenderer component
        sr = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }



    // Update is called once per frame
    void Update() {
        myAnim.SetBool("OnLand", onLand);
        myAnim.SetBool("InSpace", isInSpace);

        if (onLand) {
            GroundBehaviour();
        }
        else if (isInSpace) {
            if(metorParent.gameObject.activeInHierarchy == false)
            {
                metorParent.gameObject.SetActive(true);
            }
            Fire(weaponDamage);
            MetoriteCooldown();
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
                                break;
                            case TouchPhase.Moved:
                                //MouseDetect();
                                //LandBehaviour();
                                LandBehaviour();
                                LandMove(Camera.main.ScreenToWorldPoint(t.position));
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
                    manager.SwitchMode();
                }
                if (onLand) {
                    GroundBehaviour();
                    if (Input.GetMouseButton(0)) {
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

        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = Camera.main.transform.position.z; // Set the mouse's z-coordinate to match the camera's z-coordinate
        //Vector3 touchPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Control boundaries 
        //float minY = bottom.position.y;
        //float maxY = top.position.y;
        //float clampedY = Mathf.Clamp(touchPosition.y, minY, maxY);

        //Vector3 newPosition = transform.position;
        //newPosition.y = clampedY;

        // Lerp to the position of mouse
        //float lerpSpeed = 2f;
        //transform.position = Vector3.Lerp(transform.position, newPosition, lerpSpeed * Time.deltaTime);


        // Control boundaries 
        //float miny = bottom.position.y;
        //float maxy = top.position.y;
        //float clampedy = mathf.clamp(touchposition.y, miny, maxy);

        //vector3 newposition = transform.position;
        //newposition.y = clampedy;

        //lerp to the position of touch
        //float lerpspeed = 2f;
        //transform.position = vector3.lerp(transform.position, newposition, lerpspeed * time.deltatime);
    }

    public void LandMove(Vector3 touchPosition) {
        float minY = bottom.position.y;
        float maxY = top.position.y;
        float clampedY = Mathf.Clamp(touchPosition.y, minY, maxY);

        Vector3 newPosition = transform.position;
        newPosition.y = clampedY;

        // Lerp to the position of touch
        float lerpSpeed = 10f;
        transform.position = Vector3.Lerp(transform.position, newPosition, lerpSpeed * Time.deltaTime);
    }
        
    public void GroundBehaviour() {
        if (metorParent.gameObject.activeInHierarchy)
        {
            metorParent.gameObject.SetActive(false);
        }
        rb.velocity = new Vector2(runSpeed, 0f);
    }


    void MetoriteCooldown()
    {
        if (currentMetoriteCooldown >= metoriteCooldown)
        {
            textCooldown.text = "";
            metoriteImageCooldown.fillAmount = 1f;
            if (metoriteCharge >= maxCharge) return;
            GameObject metor = Instantiate(metoritePrefab, metoritePos[metoriteCharge].position, Quaternion.identity, metoritePos[metoriteCharge]);
            metorites.Add(metor);
            metor.GetComponent<MetoriteRotate>().pivotObject = metorParent.gameObject;
            metoriteCharge++;
            amountCharge.text = metoriteCharge.ToString();
            currentMetoriteCooldown = 0f;
            if( metor == metorites[0])
            {
                metor.GetComponent<SpriteRenderer>().sprite = selectMetor;
            }
        }
        else
        {
            currentMetoriteCooldown += Time.deltaTime;
            textCooldown.text = Mathf.RoundToInt(metoriteCooldown - currentMetoriteCooldown).ToString();
            metoriteImageCooldown.fillAmount = currentMetoriteCooldown / metoriteCooldown;
        }
    }

    public void LaunchMetorite()
    {
        if (metoriteCharge <= 0) return;
        GameObject metorite = metorites[0];
        metorites.Remove(metorite);
        metorite.GetComponent<MetoriteController>().isFired = true;
        Destroy(metorite, metorDuration);
        metoriteCharge--;
        amountCharge.text = metoriteCharge.ToString();
        GameObject nextMetor = metorites[0];
        nextMetor.GetComponent<SpriteRenderer>().sprite = selectMetor;
    }

}
