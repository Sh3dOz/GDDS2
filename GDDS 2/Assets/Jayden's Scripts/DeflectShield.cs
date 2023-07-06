using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>())
        {
            if (collision.tag == "Enemy")
            {
                Bullet bullet = collision.GetComponent<Bullet>();
                Deflect(bullet);
            }
        }
    }

    void Deflect(Bullet bullet)
    {
        bullet.speed = bullet.speed * -1;
        bullet.transform.localScale = new Vector3(1f, 1f, 1f);
        bullet.tag = "Player";
        bullet.timeSpawned = 0f;
    }
}
