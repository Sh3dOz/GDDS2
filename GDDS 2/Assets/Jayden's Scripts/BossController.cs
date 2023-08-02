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
    public Transform shootPos;
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
    public GameObject phaseThreeBullet;
    public Transform bouncePos, bouncePos2, bouncePos3;
    bool chosenAction;
    bool doneWithAction;
    bool doneWithAction2;
    bool shotBubbles;
    bool shotLas;
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
                Phase3();
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

    IEnumerator ShootLaser()
    {
        if (laserPhase)
        {
            if (!shotLaser)
            {
                transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                float dist = transform.position.y - midPos.position.y;
                if (dist > Mathf.Epsilon) yield break;
                //Charging up

                yield return new WaitForSeconds(3f);
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
                    Instantiate(phaseTwoBullet, shootPos.position, Quaternion.Euler(new Vector3(0f, 0f, -15f)));
                    Instantiate(phaseTwoBullet, shootPos.position, Quaternion.Euler(new Vector3(0f, 0f, 15f)));
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
            rand = Random.Range(1, 4);
            chosenAction = true;
        }
        switch (rand)
        {
            case 1:
                if (!doneWithAction)
                {
                    //Bob
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
                    StartCoroutine(WaitAction1(3f));
                }
                else if (!doneWithAction2)
                {
                    if (!shotBubbles)
                    {
                        //Move to Mid
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                        float dist = transform.position.y - midPos.position.y;
                        if (dist > Mathf.Epsilon) return;
                    }
                    //Shoot
                    ShootBubbles();

                    //Bob
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
                    StartCoroutine(WaitAction2(5f));
                }
                else if (doneWithAction && doneWithAction2)
                {
                    //Move to Mid
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                    float dist = transform.position.y - midPos.position.y;
                    if (dist > Mathf.Epsilon) return;

                    if (!shotLas)
                    {
                        //Shoot Laser
                        GameObject laser = Instantiate(phaseTwoLaser, eyePos.position, Quaternion.identity, eyePos);
                        Destroy(laser, 10f);
                        shotLas = true;
                     
                    }
                    //Reset
                    StartCoroutine(WaitAny(10f));
                }
                break;
            case 2:
                if (!doneWithAction)
                {
                    if (!shotBubbles)
                    {
                        //Move to Mid
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                        float dist = transform.position.y - midPos.position.y;
                        if (dist > Mathf.Epsilon) return;
                    }
                    //Shoot
                    ShootBubbles();

                    //Bob
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);
                    StartCoroutine(WaitAction1(5f));
                }
                else if (!doneWithAction2)
                {
                    //Move to Mid
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                    float dist = transform.position.y - midPos.position.y;
                    if (dist > Mathf.Epsilon) return;

                    if (!shotLas)
                    {
                        //Shoot Laser
                        GameObject laser = Instantiate(phaseTwoLaser, eyePos.position, Quaternion.identity, eyePos);
                        Destroy(laser, 10f);
                        shotLas = true;
                    }
                    //Reset
                    StartCoroutine(WaitAction2(10f));
                }
                else if (doneWithAction && doneWithAction2)
                {
                    //Reset
                    ResetPhase3();
                }
                break;
            case 3:
                if (!doneWithAction)
                {
                    //Move to Mid
                    transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                    float dist = transform.position.y - midPos.position.y;
                    if (dist > Mathf.Epsilon) return;

                    if (!shotLas)
                    {
                        //Shoot Laser
                        GameObject laser = Instantiate(phaseTwoLaser, eyePos.position, Quaternion.identity, eyePos);
                        Destroy(laser, 10f);
                        shotLas = true;
                    }
                    //Reset
                    StartCoroutine(WaitAction1(10f));

                }
                else if (!doneWithAction2)
                {
                    //Bob
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);

                    StartCoroutine(WaitAction2(3f));
                }
                else if (doneWithAction && doneWithAction2)
                {
                    if (!shotBubbles)
                    {
                        //Move to Mid
                        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, midPos.position.y, 1f), transform.position.z);
                        float dist = transform.position.y - midPos.position.y;
                        if (dist > Mathf.Epsilon) return;
                    }
                    //Shoot
                    ShootBubbles();

                    //Bob
                    transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, topLimit.position.y - botLimit.position.y) + botLimit.position.y);

                    //Reset
                    StartCoroutine(WaitAny(5f));
                }
                break;
        }
    }

    void ShootBubbles()
    {
        if (shotBubbles) return;
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-117f));
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-142f));
        Instantiate(phaseThreeBullet, bouncePos.position, Quaternion.Euler(0f,0f,-65f));

        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 117f));
        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 70f));
        Instantiate(phaseThreeBullet, bouncePos2.position, Quaternion.Euler(0f, 0f, 34f));

        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, 39f));
        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, -10f));
        Instantiate(phaseThreeBullet, bouncePos3.position, Quaternion.Euler(0f, 0f, -52f));
        shotBubbles = true;
    }

    IEnumerator WaitAction1(float duration)
    {
        Debug.Log("Wait 1");
        yield return new WaitForSeconds(duration);
        doneWithAction = true;
        StopAllCoroutines();
    }

    IEnumerator WaitAction2(float duration)
    {
        Debug.Log("Wait 2");
        yield return new WaitForSeconds(duration);
        doneWithAction2 = true;
        StopAllCoroutines();
    }
    IEnumerator WaitLaser()
    {
        Debug.Log("Wait Laser");
        yield return new WaitForSeconds(laserDuration);
        shotLaser = false;
        laserPhase = false;
    }
    IEnumerator WaitShoot()
    {
        Debug.Log("Wait bubbles");
        yield return new WaitForSeconds(5f);
        laserPhase = true;
    }

    IEnumerator WaitAny(float duration)
    {
        Debug.Log("Wait Reset");
        yield return new WaitForSeconds(duration);
        ResetPhase3();
    }

    void ResetPhase3()
    {
        chosenAction = false;
        doneWithAction = false;
        doneWithAction2 = false;
        shotLas = false;
        shotBubbles = false;
        StopAllCoroutines();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
