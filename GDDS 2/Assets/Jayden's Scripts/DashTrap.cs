
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrap : MonoBehaviour
{
    float dashSpeed = -10f;
    bool isDashing = false;
    public Transform dashPos;
    bool dashWait;
    float dashDuration;
    public GameObject target;
    public int damage;
    Animator myAnim;
    public bool isActivated;
    bool killActive;
    public bool isTracking;
    bool spawnedDanger;
    public GameObject dangerSign;
    public Transform dangerSpawn;
    public AudioSource UI;
    public AudioClip tickingSound;
    public AudioClip missileSound;
    public bool isLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        dashDuration = isTracking ? 1f : 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isActivated)
        {
            if (killActive) {
                Destroy(this.gameObject, 10f);
                killActive = false;
            }
                if (!spawnedDanger)
            {
                
                GameObject danger = Instantiate(dangerSign, new Vector3(dangerSpawn.position.x, transform.position.y, 0f), Quaternion.identity, this.transform);
                danger.transform.position = new Vector3(danger.transform.position.x, danger.transform.position.y, 0f);
                spawnedDanger = true;
                UI.PlayOneShot(tickingSound);
                Destroy(danger, dashDuration);
            }
            if (isTracking)
            {
                if(isDashing == false)
                {
                    transform.position = new Vector3(dashPos.position.x, dashPos.position.y, transform.position.z);
                }
                else if(isDashing == true)
                {
                    transform.position = new Vector3(transform.position.x + (dashSpeed * Time.deltaTime), transform.position.y, transform.position.z);
                }
            }
            if (isDashing == false)
            {
                transform.position = new Vector3(dashPos.position.x, target.transform.position.y, transform.position.z);
            }
            else if (isDashing == true)
            {
                StartCoroutine("DrillLaunch");
                transform.position = new Vector3(transform.position.x + (dashSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }

    IEnumerator DashWait()
    {
        isActivated = true;
        target = FindObjectOfType<PlayerController>().gameObject;
        dashWait = true;
        if (isTracking)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        dashWait = false;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        isDashing = true;
    }

    public IEnumerator DrillLaunch() {
        if(isLaunched) {
            yield break;
        }
        UI.PlayOneShot(missileSound);
        isLaunched = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (collision.GetComponent<CameraCollider>())
        {
            myAnim.enabled = true;
            StartCoroutine("DashWait");
        }
        else if(collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }

}
