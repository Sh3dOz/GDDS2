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

    public AudioSource audioS;
    public AudioClip warpSound;
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
        yield return new WaitForSeconds(1f);
        audioS.PlayOneShot(warpSound);

        warpParticle.SetActive(true);
        warpModule = warp.main;
        yield return new WaitForSeconds(1f);

        warpModule.startSpeed = 20;
        yield return new WaitForSeconds(1f);
        warpModule.startSpeed = 40;

        yield return new WaitForSeconds(1f);

        cameraDirector.Play();
        yield return new WaitForSeconds(0.5f);
        ceiling.SetActive(false);
        floor.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        virtualCamera.Follow = newTarget;


    }
}
