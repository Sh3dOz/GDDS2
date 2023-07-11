using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrapController : MonoBehaviour
{
    public AudioSource audioS;
    public AudioClip shockSound;
    public GameObject shockEffect;
    public PlayerController player;


    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.GetComponent<PlayerController>();
        if (p)
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
            audioS.PlayOneShot(shockSound);
            Instantiate(shockEffect, p.transform.position, Quaternion.identity);
        }
        
        if(collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }
}
