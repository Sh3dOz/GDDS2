using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;

public class TransitionToSpaceship : MonoBehaviour {

    public GameObject spaceship;
    public GameObject player;
    public GameObject spaceshipDup;
    public bool transitioned = false;
    public GameObject joyStick;
    public CameraTargetChanger cameras;
    public CinemachineVirtualCamera vcam1;

    public Sprite spaceshipSprite;

    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;
    public PlayableDirector playableDirector3;

    public PlayerController spaceships;
    public LevelManager playerSwitch;

    public AudioSource audioS;
    public AudioClip warpSound;
    public ParticleSystem warp;
    public GameObject warpParticle;
    public GameObject speedWarp;
    public ParticleSystem.MainModule warpModule;

    public GameObject ceiling;
    public GameObject floor;
    public GameObject walls;


    // Start is called before the first frame update
    void Start() {
        cameras = FindObjectOfType<CameraTargetChanger>();
        spaceships = FindObjectOfType<PlayerController>();
        playerSwitch = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (this.GetComponent<PlayerController>()) return;
        if (collision.tag == "Player") {
            Transition();
            if(collision.name == "Korg") {
                playableDirector.Play();
            }
            else if (collision.name == "Axel") {
                playableDirector2.Play();
            }
            else if (collision.name == "Xav") {
                playableDirector3.Play();
            }
            Debug.Log("Transg");
        }
    }

    public void Transition() {
        vcam1.Priority = 9;
        playableDirector.Play();
        spaceshipDup.GetComponent<SpriteRenderer>().sprite = null;
        StartCoroutine("Warp");
    }


    public IEnumerator Warp() {
        audioS.PlayOneShot(warpSound);

        warpParticle.SetActive(true);
        warpModule = warp.main;
        yield return new WaitForSeconds(0.5f);

        warpModule.startSpeed = 14;
        yield return new WaitForSeconds(1f);
        warpModule.startSpeed = 24;

        Debug.Log("Playing");

        yield return new WaitForSeconds(1.4f);
        ceiling.SetActive(false);
        floor.SetActive(false);
        walls.SetActive(false);
        speedWarp.SetActive(true);
    }

    void SwitchDup()
    {
        spaceshipDup.GetComponent<SpriteRenderer>().sprite = null;
    }

    void SwitchMode()
    {
        playerSwitch.SwitchMode();
        vcam1.Priority = 11;
        StartCoroutine(WaitLol());
        Destroy(spaceshipDup);
    }

    IEnumerator WaitLol()
    {
        yield return new WaitForSeconds(2f);
    }
}
