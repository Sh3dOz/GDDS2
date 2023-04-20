using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float timeSpawned;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawned += Time.deltaTime;
        if(timeSpawned > 5f)
        {
            Destroy(gameObject);
        }
    }
}
