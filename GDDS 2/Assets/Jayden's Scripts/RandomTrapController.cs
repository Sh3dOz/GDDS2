using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrapController : MonoBehaviour
{
    public List<Traps> possibleTraps;
    public SpriteRenderer sr;
    int damage;
    string trapName;
    Traps currentTrap;
    float moveSpeed;
    bool isDashing = false;
    public Transform dashPos;
    bool dashWait;
    public GameObject target;

    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(startPos.position.x,startPos.position.y);
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
                break;
            case ("Dash"):
                moveSpeed = currentTrap.moveSpeed;
                StartCoroutine(DashWait());
                if(isDashing == false)
                {
                    transform.position = new Vector3(dashPos.position.x, target.transform.position.y, transform.position.z);
                }
                else if (isDashing == true)
                {
                    transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y , transform.position.z);
                }
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

    IEnumerator DashWait()
    {
        if (dashWait == true) yield break;
        dashWait = true;
        yield return new WaitForSeconds(3f);
        isDashing = true;
    }

    public void ResetTrap()
    {
        transform.position = new Vector2(startPos.position.x, startPos.position.y);
        isDashing = false;
        dashWait = false;
        trapName = null;
        GenerateTrap();
    }
}
