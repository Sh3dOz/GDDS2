
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
    public bool isTracking;
    bool spawnedDanger;
    public GameObject dangerSign;
    public Transform dangerSpawn;
    public AudioSource UI;
    public AudioClip missileSound;

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
            if (!spawnedDanger)
            {
                GameObject danger = Instantiate(dangerSign, dangerSpawn.position, Quaternion.identity, this.transform);
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
                transform.position = new Vector3(transform.position.x + (dashSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }

    IEnumerator DashWait()
    {
        isActivated = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
