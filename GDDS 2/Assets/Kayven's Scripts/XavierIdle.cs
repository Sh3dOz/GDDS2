using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XavierIdle : MonoBehaviour {

    private Animator anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
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
                anim.SetTrigger("XavierIdle1");
            }
            else if (randomNumber == 2) {
                anim.SetTrigger("XavierIdle2");
            }
        }
    }
}
