using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraTargetChanger : MonoBehaviour {
    public Transform newTarget;
    public CinemachineVirtualCamera virtualCamera;
    public TransitionToSpaceship spaceship;

    void Start() {
        spaceship = FindObjectOfType<TransitionToSpaceship>();    
    }
    void Update() {
        if (spaceship.transitioned == true) {
            ChangeCameraTarget();
        }
    }
    void ChangeCameraTarget() {
        virtualCamera.Follow = newTarget;
    }
}
