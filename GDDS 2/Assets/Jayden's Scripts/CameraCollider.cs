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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BasicEnemy>())
        {
            collision.GetComponent<BasicEnemy>().isActivated = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RandomTrapController>())
        {
            collision.GetComponent<RandomTrapController>().ResetTrap();
        }
        if (collision.GetComponent<BasicEnemy>())
        {
            collision.GetComponent<BasicEnemy>().isActivated = false;
        }
        if (collision.GetComponent<DashTrap>() && collision.GetComponent<DashTrap>().isActivated == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
