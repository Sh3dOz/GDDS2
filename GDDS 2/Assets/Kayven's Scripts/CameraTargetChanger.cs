using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;


public class CameraTargetChanger : MonoBehaviour {

    public Transform transition;
    public Transform newTarget;
    public CinemachineVirtualCamera virtualCamera;
    public TransitionToSpaceship spaceship;

    public PlayableDirector cameraDirector;

    public float smoothing = 2f;

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

    public void CameraTransition() {
        virtualCamera.Follow = null;
        StartCoroutine("CameraToSpace");
    }

    public IEnumerator CameraToSpace() {
        yield return new WaitForSeconds(2f);
        cameraDirector.Play();
    }
}
