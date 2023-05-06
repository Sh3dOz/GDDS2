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
    bool isDashing = false;
    public Transform dashPos;
    bool dashWait;
    public GameObject target;
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
                Debug.Log("Trap is buzzsaw");
                break;
            case ("Dash"):
                Debug.Log("Trap is Dash");
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
        Debug.Log("wait");
        dashWait = true;
        yield return new WaitForSeconds(3f);
        Debug.Log("done waiting");
        isDashing = true;
    }
}
