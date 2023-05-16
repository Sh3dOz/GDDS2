using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzspike : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
