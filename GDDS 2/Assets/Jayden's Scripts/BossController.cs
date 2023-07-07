using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    enum PhaseMode {One, Second,Third};
    PhaseMode currentPhase;

    [Header("Shooting")]
    public Transform eyePos, topLimit, botLimit;
    public GameObject pharseOneBullet;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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

    }
}
