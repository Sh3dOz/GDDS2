using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    float width = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            width = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
            Vector3 temp = collision.transform.position;
            temp.x += width * 3;
            collision.transform.position = temp;
        }
    }
}
