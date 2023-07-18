using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    enum PhaseMode {One, Second,Third};
    PhaseMode currentPhase;

    [Header("Phase1")]
    public Transform eyePos, topLimit, botLimit;
    public GameObject phaseOneBullet;
    float shootTimer;

    [Header("Phase2")]
    public Transform midPos;
    public Transform topG;
    public Transform botG;
    public GameObject phaseTwoLaser;
    public GameObject phaseTwoBullet;
    float laserDuration;
    bool shotLaser;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        shootTimer = Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        PharseCheck();
        switch (currentPhase)
        {
            case PhaseMode.One:
                ShootEyes();
                break;
            case PhaseMode.Second:
                ShootLaser();
                break;
            case PhaseMode.Third:
                break;
            default:
                break;
        }
    }

    void PharseCheck()
    {
        if (currentHealth >= maxHealth * 0.75)
        {
            currentPhase = PhaseMode.One;
        }
        else if(currentHealth >= maxHealth * 0.25)
        {
            currentPhase = PhaseMode.Second;
        }
        else
        {
            currentPhase = PhaseMode.Third;
        }
    }

    void ShootEyes()
    {
        if(shootTimer <= 0f)
        {
            GameObject laser = Instantiate(phaseOneBullet, eyePos.position, Quaternion.identity, eyePos);
            shootTimer = Random.Range(2, 6);
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
    }

    void ShootLaser()
    {
        if (!shotLaser)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
            //Shoot laser
            GameObject topLaser = Instantiate(phaseTwoLaser, topG.position, Quaternion.identity);
            GameObject botLaser = Instantiate(phaseTwoLaser, botG.position, Quaternion.identity);
            Destroy(topLaser, laserDuration);
            Destroy(botLaser, laserDuration);
            shotLaser = true;
            StartCoroutine(WaitLaser());
        }
        if (shotLaser)
        {
            //Shoot Bouncing Bullets
            Instantiate(phaseTwoBullet, eyePos.position, Quaternion.Euler(new Vector3(0f,0f,-15f)));
            Instantiate(phaseTwoBullet, eyePos.position, Quaternion.Euler(new Vector3(0f, 0f, 15f)));
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
        }
    }

    IEnumerator WaitLaser()
    {
        yield return new WaitForSeconds(laserDuration + 5f);
        shotLaser = false;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < maxHealth)
        {
            Die();
        }
    }

    void Die()
    {

    }
}
