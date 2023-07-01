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

    public GameObject warpParticle;
    public float smoothing = 2f;
    public ParticleSystem warp;
    public ParticleSystem.MainModule warpModule;

    public GameObject ceiling;
    public GameObject floor;

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
        warpParticle.SetActive(true);
        warpModule = warp.main;
        yield return new WaitForSeconds(1f);

        warpModule.startSpeed = 14;
        yield return new WaitForSeconds(1f);
        warpModule.startSpeed = 28;


        cameraDirector.Play();
        yield return new WaitForSeconds(0.5f);
        ceiling.SetActive(false);
        floor.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        virtualCamera.Follow = newTarget;


    }
}
