using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ToiletSound : MonoBehaviour {

    public AudioSource audioS;
    public AudioClip toiletSound;
    public GameObject upgradesMenu;

    public PlayableDirector toiletBreak;

    // Start is called before the first frame update
    void Start()
    {
        audioS = FindObjectOfType<AudioSource>();
        toiletBreak.Play();
        StartCoroutine("ToiletBreak");
    }

    public IEnumerator ToiletBreak() {
        yield return new WaitForSeconds(5.5f);
        audioS.PlayOneShot(toiletSound);
        yield return new WaitForSeconds(4.5f);
        upgradesMenu.SetActive(true);
    }
}
