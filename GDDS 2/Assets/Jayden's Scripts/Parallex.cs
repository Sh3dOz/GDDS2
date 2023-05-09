using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    float length, startPos;
    public GameObject cam;
    public float parallexEffect;
    public bool autoScroll = false;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));
        float dist = (cam.transform.position.x * parallexEffect);
        float desiredXPos = startPos + dist;

        if (autoScroll)
        {
            desiredXPos = transform.position.x - parallexEffect;
        }
        transform.position = new Vector2(desiredXPos, transform.position.y);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
