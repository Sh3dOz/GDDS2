using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetoriteController : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isFired;
    Animator myAnim;
    public float metorSpeed;
    public int metorDamage;
    public float angleChangingSpeed = 200f;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetBool("isFired", isFired);
        if (isFired)
        {
            target = TargetCheck();
            GetComponent<MetoriteRotate>().enabled = false;
            transform.rotation = Quaternion.identity;
        }
    }

    private void FixedUpdate()
    {
        if (isFired)
        {
            Vector2 direction = (Vector2)target.transform.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = angleChangingSpeed * -rotateAmount;
            rb.velocity = new Vector3(metorSpeed, 0f);
        }
    }

    public GameObject TargetCheck()
    {
        if (target != null) return target.gameObject;
        List<SpaceEnemy> currentEnemies;
        float distance = Mathf.Infinity;
        GameObject closest = null;
        currentEnemies = new List<SpaceEnemy>(FindObjectsOfType<SpaceEnemy>());
        foreach (SpaceEnemy i in currentEnemies)
        {
            Vector3 diff = i.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = i.gameObject;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceEnemy>())
        {
            if (collision.GetComponent<BossController>())
            {
                collision.GetComponent<BossController>().TakeDamage(metorDamage);
            }
            else
            {
                collision.GetComponent<SpaceEnemy>().TakeDamage(metorDamage);
            }
            Destroy(gameObject);
        }
        if (collision.GetComponent<Bullet>())
        {
            if(collision.tag == "Enemy")
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
