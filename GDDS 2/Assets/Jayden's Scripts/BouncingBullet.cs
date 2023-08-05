using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioS = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyBullet();
        Movement();
    }

    public override void DestroyBullet()
    {
        timeSpawned += Time.deltaTime;
        if (timeSpawned > 5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<CameraCollider>())
        {
            if (collision.gameObject.GetComponent<CameraCollider>().isBullet)
            {
                rb.velocity = Vector3.Reflect(rb.velocity, collision.GetContact(0).normal);
                //Vector3 rotationToAdd = new Vector3(0f, 0f, (360f - transform.eulerAngles.z) * 2);
                //transform.Rotate(rotationToAdd);
            }
        }
    }
}
