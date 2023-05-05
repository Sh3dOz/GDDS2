using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    public List<Traps> possibleTraps;
    public SpriteRenderer sr;
    int damage;
    string trapName;
    Traps currentTrap;
    float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GenerateTrap();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTrapBehaviour(trapName);
    }

    public void GenerateTrap()
    {
        int rand = Random.Range(0, possibleTraps.Count);
        currentTrap = possibleTraps[rand];
        sr.sprite = currentTrap.sprite;
        damage = currentTrap.damage;
        trapName = currentTrap.trapName;
    }

    public void GenerateTrapBehaviour(string trapName)
    {
        switch (trapName)
        {
            case ("Buzzsaw"):
            case ("Dash"):
                moveSpeed = currentTrap.moveSpeed;
                transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y, transform.position.z);
                break;
            default: break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
