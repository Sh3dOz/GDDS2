
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTrap : MonoBehaviour
{
    float moveSpeed;
    bool isDashing = false;
    public Transform dashPos;
    bool dashWait;
    public GameObject target;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Movement()
    {
        StartCoroutine(DashWait());
        if (isDashing == false)
        {
            transform.position = new Vector3(dashPos.position.x, target.transform.position.y, transform.position.z);
        }
        else if (isDashing == true)
        {
            transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    IEnumerator DashWait()
    {
        if (dashWait == true) yield break;
        dashWait = true;
        yield return new WaitForSeconds(3f);
        isDashing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
