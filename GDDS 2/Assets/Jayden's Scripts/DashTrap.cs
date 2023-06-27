
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrap : MonoBehaviour
{
    float dashSpeed = -10f;
    bool isDashing = false;
    public Transform dashPos;
    bool dashWait;
    public GameObject target;
    public int damage;
    Animator myAnim;
    bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
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
            if (isDashing == false)
            {
                transform.position = new Vector3(dashPos.position.x, target.transform.position.y, transform.position.z);
            }
            else if (isDashing == true)
            {
                transform.position = new Vector3(transform.position.x + (dashSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            }
            Debug.Log("yes?");
        }
    }

    IEnumerator DashWait()
    {
        isActivated = true;
        dashWait = true;
        yield return new WaitForSeconds(3f);
        dashWait = false;
        GetComponent<PolygonCollider2D>().isTrigger = true;
        isDashing = true;
        Debug.Log("done?");
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
    }
}
