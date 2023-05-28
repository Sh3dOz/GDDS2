using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosingScreen : MonoBehaviour {
    public KorgController player;
    public GameObject losingScreens;
    public AudioSource UI;
    public AudioClip crash;
    public AudioSource levelMusic;
    public bool deadedCoStarted = false;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<KorgController>();   
    }

    // Update is called once per frame
    void Update() {
        if(player.health <= 0) {
            StartCoroutine("Crash");
            StartCoroutine("LosingScreene");


        }
    }

    public IEnumerator LosingScreene() {       
        levelMusic.Stop();
        yield return new WaitForSeconds(0.2f);
        losingScreens.SetActive(true);
            
    }

    public IEnumerator Crash() {
        if (deadedCoStarted) {
            yield break;
        }
        deadedCoStarted = true;
        UI.PlayOneShot(crash);
    }
}
