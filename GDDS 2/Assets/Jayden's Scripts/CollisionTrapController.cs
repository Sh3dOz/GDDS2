using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrapController : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip shockSound;
    public GameObject shockEffect;
    public PlayerController[] players;


    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<PlayerController>())
        {
            foreach (PlayerController player in players) {
                if (player.canBeDamaged == true) {
                    collision.GetComponent<PlayerController>().TakeDamage(damage);
                    audioS.PlayOneShot(shockSound);
                    Instantiate(shockEffect, player.transform.position, Quaternion.identity);
                }
            }
            Debug.Log("ZAPped");
        }
        if(collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }
}
