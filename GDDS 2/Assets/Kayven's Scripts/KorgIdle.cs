using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorgIdle : MonoBehaviour {

    private Animator anim;
    public bool isToilet = false;
    public Toilet toilet;
    public AudioSource UI;
    public AudioClip toiletTime;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        UI = FindObjectOfType<AudioSource>();
        toilet = FindObjectOfType<Toilet>();
        StartCoroutine(GenerateIdleBehaviour());
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator GenerateIdleBehaviour() {
        while (true) {
            yield return new WaitForSeconds(15f);

            // Generate random number between 1 and 2
            int randomNumber = Random.Range(1, 3);
            if (randomNumber == 1) {
                anim.SetTrigger("KorgIdle1");
            }
            else if (randomNumber == 2) {
                anim.SetTrigger("KorgIdle2");
            }
        }
    }

    public void ToiletBreak() {
        if (!isToilet) {
            toilet.anim.SetTrigger("ToiletDrop");
            isToilet = true;
            isToilet = false;
        }
    }

    public void ToiletTime() {
        UI.PlayOneShot(toiletTime);
    }
}
