using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour
{
    //public UpgradesAgain upgrade;
    //public KorgIdle korg;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(upgrade.index == 0 && korg.isToilet == true) {
            //anim.SetTrigger("ToiletDrop");
        //}
    }
}
