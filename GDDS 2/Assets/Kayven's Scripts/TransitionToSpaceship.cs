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

    public PlayableDirector playableDirector;
    private bool isTimelineStarted = false;


    // Start is called before the first frame update
    void Start() {
        cameras = FindObjectOfType<CameraTargetChanger>();
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
        }
    }

    public IEnumerator Transition() {
        yield return new WaitForSeconds(0.5f);
        joyStick.SetActive(true);
        player.SetActive(false);
        spaceship.SetActive(true);
        spaceshipDup.SetActive(false);
        cameras.CameraTransition();

    }
}
