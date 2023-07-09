using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransitionToSpaceship : MonoBehaviour {

    public GameObject spaceship;
    public GameObject player;
    public GameObject spaceshipDup;
    public bool transitioned = false;
    public GameObject joyStick;
    public CameraTargetChanger cameras;

    public Sprite spaceshipSprite;

    public PlayableDirector playableDirector;
    private bool isTimelineStarted = false;
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
        if (spaceship.activeSelf && isTimelineStarted) {
            playableDirector.Play();
            isTimelineStarted = true;
            
        }

    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {           
            StartCoroutine("Transition");
            Debug.Log("Transg");
        }
    }

    public IEnumerator Transition() {
        Object.Destroy(spaceshipDup);
        spaceships.playerSprite = spaceshipSprite;
        yield return new WaitForSeconds(4.5f);
        playerSwitch.SwitchMode();
    }
}
