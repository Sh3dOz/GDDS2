using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeStart : MonoBehaviour
{
    // Start is called before the first frame update
    public DialougeTrigger trigger;


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            trigger.StartDialouge();
        }

    }
}

