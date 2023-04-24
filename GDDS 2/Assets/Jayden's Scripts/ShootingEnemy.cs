using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector3 offset = new Vector3(10f, 0f, 0f);
    public float size = 10f;
    public int bulletCount;
    public int maxBullet;
    public bool isReloading;
    public Transform shootPos;
    public float fireRate;
    public float nextFire;
    // Start is called before the first frame update
   
}
