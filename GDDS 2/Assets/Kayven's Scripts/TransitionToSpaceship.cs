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
    public PlayerController spaceships;
    public LevelManager playerSwitch;


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
            Debug.Log("Transg");
        }
    }

    public void Transition() {
        vcam1.Priority = 9;
        playableDirector.Play();
        spaceshipDup.GetComponent<SpriteRenderer>().sprite = null;
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
