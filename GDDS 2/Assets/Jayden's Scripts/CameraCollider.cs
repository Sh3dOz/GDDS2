using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<TrapController>())
        {
            collision.GetComponent<TrapController>().ResetTrap();
        }
    }
}