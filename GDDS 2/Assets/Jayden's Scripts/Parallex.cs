using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    float length, startPos;
    public GameObject cam;
    public float parallexEffect;
    public bool autoScroll = false;
    public float temp;
    public float dist;
    public float desiredXPos;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        temp = (cam.transform.position.x * (1 - parallexEffect));
        dist = (cam.transform.position.x * parallexEffect);
        desiredXPos = startPos + dist;

        if (autoScroll)
        {
            desiredXPos = transform.position.x - parallexEffect;
        }
        transform.position = new Vector2(desiredXPos, transform.position.y);

        if (temp > startPos + length)
        {
            startPos += length;
            transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
        else if (temp < startPos - length)
        {
            startPos -= length; transform.position = new Vector3(startPos, transform.position.y, transform.position.z);
        }
    }
}
