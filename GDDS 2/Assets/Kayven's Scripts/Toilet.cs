using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    //public UpgradesAgain upgrade;
    //public KorgIdle korg;
    public Animator anim;
    public AudioSource UI;
    public AudioClip doorOpen;
    public AudioClip doorClose;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UI = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(upgrade.index == 0 && korg.isToilet == true) {
            //anim.SetTrigger("ToiletDrop");
        //}
    }

    public void OpenDoor() {
        UI.PlayOneShot(doorOpen);
    }

    public void CloseDoor() {
        UI.PlayOneShot(doorClose);
    }
}
