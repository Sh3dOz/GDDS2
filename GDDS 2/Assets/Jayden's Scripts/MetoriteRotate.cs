using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetoriteRotate : MonoBehaviour
{
    public GameObject pivotObject;
    public float rotationSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0f, 0f, 1f), rotationSpeed * Time.deltaTime);
    }
}
