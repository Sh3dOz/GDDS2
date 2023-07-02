using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public float angleChangingSpeed = 2f;
    public float movementSpeed = 10f;
    int damage = 10;
    float timeSpawned;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        target = TargetCheck().transform;
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 2f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -angleChangingSpeed * rotateAmount;
        rb.velocity = transform.forward * movementSpeed;
    }

    public GameObject TargetCheck()
    {
        List<SpaceEnemy> currentEnemies;
        float distance = Mathf.Infinity;
        GameObject closest = null;
        currentEnemies = new List<SpaceEnemy>(FindObjectsOfType<SpaceEnemy>());
        Debug.Log(currentEnemies);
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
        Debug.Log(closest);
        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SpaceEnemy>())
        {
            collision.GetComponent<SpaceEnemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
