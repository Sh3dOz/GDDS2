using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    public Transform centerPoint;
    public float rotationSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(centerPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}

