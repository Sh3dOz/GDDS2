using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public List<Traps> possibleTraps;
    public SpriteRenderer sr;
    Traps currentTrap;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateTrap()
    {
        int rand = Random.Range(0, possibleTraps.Count);
        currentTrap = possibleTraps[rand];
        sr.sprite = currentTrap.sprite;
    }
}
