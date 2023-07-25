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
