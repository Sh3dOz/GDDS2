using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : ShootingEnemy
{
    public Transform origin;
    public Vector2 boxSize;
    public LayerMask whoToShoot;
    public bool isActivated;
    LevelManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isWin == false) isActivated = false;
        if (isActivated)
        {
            Shoot();
        }
    }
}
