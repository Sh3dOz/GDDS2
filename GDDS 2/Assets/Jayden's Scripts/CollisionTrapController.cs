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
        if (collision.GetComponentInParent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);           
            Instantiate(shockEffect, player.transform.position, Quaternion.identity);
            audioS.PlayOneShot(shockSound);
            Debug.Log("ZAPped");
        }
        if(collision.tag == "EMP")
        {
            Destroy(this.gameObject);
        }
    }
}
