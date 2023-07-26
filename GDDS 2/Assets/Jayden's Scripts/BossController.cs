using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    enum PhaseMode {One, Second,Third};
    PhaseMode currentPhase;

    Animator myAnim;

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
    float laserDuration = 5f;
    bool laserPhase;
    bool shotLaser;
    float nextFire;
    public float fireRate;
    float fireTimer;

    [Header("Phase3")]
    public Transform bouncePos, bouncePos2, bouncePos3;
    public GameObject phaseThreeBullet;
    bool chosenAction;
    bool doneWithAction;
    bool doneWithAction2;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        shootTimer = Random.Range(2, 6);
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("Health", currentHealth);
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
        if (laserPhase)
        {
            if (!shotLaser)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                float dist = transform.position.x - midPos.position.x;
                if (dist > Mathf.Epsilon) return;
                //Shoot laser
                GameObject topLaser = Instantiate(phaseTwoLaser, topG.position, Quaternion.identity, topG.transform);
                GameObject botLaser = Instantiate(phaseTwoLaser, botG.position, Quaternion.identity, botG.transform);
                Destroy(topLaser, laserDuration);
                Destroy(botLaser, laserDuration);
                shotLaser = true;
                StartCoroutine(WaitLaser());
            }
            if (shotLaser)
            {
                //Shoot Bouncing Bullets
                if (fireTimer > nextFire)
                {
                    nextFire = fireTimer + fireRate;
                    Instantiate(phaseTwoBullet, eyePos.position, Quaternion.Euler(new Vector3(0f, 0f, -15f)));
                    Instantiate(phaseTwoBullet, eyePos.position, Quaternion.Euler(new Vector3(0f, 0f, 15f)));
                }
                fireTimer += Time.deltaTime;
            }
        }
        else
        {
            ShootEyes();
            StartCoroutine(WaitShoot());
        }
    }

    void Phase3()
    {
        if (chosenAction == false)
        {
            rand = Random.Range(1, 3);
            chosenAction = true;
        }
        switch (rand)
        {
            case 1:
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
                break;
            case 2:
                //Move to Mid
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                float dist = transform.position.x - midPos.position.x;
                if (dist > Mathf.Epsilon) return;
                
                //Shoot
                ShootBubbles();

                //Bob
                transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
                
                //Move to Mid
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                dist = transform.position.x - midPos.position.x;
                if (dist > Mathf.Epsilon) return;
                
                //Shoot Laser
                Instantiate(phaseOneBullet, eyePos.position, Quaternion.identity, eyePos);

                //Reset
                chosenAction = false;
                break;
            case 3:
                //Laser
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                dist = transform.position.x - midPos.position.x;
                if (dist > Mathf.Epsilon) return;
                Instantiate(phaseOneBullet, eyePos.position, Quaternion.identity, eyePos);
                break;
        }
    }

    void ShootBubbles()
    {
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-117f));
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-142f));
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-65f));

        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 117f));
        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 70f));
        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 34f));

        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, 39f));
        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, -10f));
        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, -52f));
    }

    IEnumerator WaitLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        shotLaser = false;
        laserPhase = false;
    }
    IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(5f);
        laserPhase = true;
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
