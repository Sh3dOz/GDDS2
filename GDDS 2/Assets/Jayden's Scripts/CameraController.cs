using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    Camera mainCam;
    CinemachineVirtualCamera vcam1;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GetComponent<Camera>();
        vcam1 = FindObjectOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
