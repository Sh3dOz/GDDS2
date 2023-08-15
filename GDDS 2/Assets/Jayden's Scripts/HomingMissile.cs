using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public Transform target;
    public Rigidbody2D rb;
    public float angleChangingSpeed = 200f;
    public float movementSpeed = 10f;
    int damage = 10;
    float timeSpawned;

    public GameObject explosion;
    public AudioClip explodeAound;
    public LevelManager manager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = FindObjectOfType<LevelManager>();
    }
    private void Update()
    {
        target = TargetCheck().transform;
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 10f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.transform.position - rb.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = angleChangingSpeed * -rotateAmount;
        rb.velocity = transform.up * movementSpeed;
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
        if (this.tag == "Player")
        {
            Debug.Log("haro?");
            Debug.Log(collision.gameObject);
            if (collision.GetComponent<BossController>())
            {
                collision.GetComponent<BossController>().TakeDamage(damage);
                Instantiate(explosion, transform.position, Quaternion.identity);
                manager.audioS.PlayOneShot(explodeAound);
                Destroy(gameObject);
            }
            else if (collision.GetComponent<SpaceEnemy>())
            {
                collision.GetComponent<SpaceEnemy>().TakeDamage(damage, this.gameObject);
                Instantiate(explosion, transform.position, Quaternion.identity);
                manager.audioS.PlayOneShot(explodeAound);
                Destroy(gameObject);
            }
        }
    }
}
