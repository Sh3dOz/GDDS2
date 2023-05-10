using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform shootPos;
    public float fireRate;
    protected float nextFire;
    public int damage;

    public abstract void Fire();
   
}
