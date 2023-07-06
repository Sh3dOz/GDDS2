using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    float moveSpeed = 20f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Work?");
        if(other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
        }
    }
}
