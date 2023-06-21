using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : Weapon
{
    public override void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.Euler(-45f, 0f, 0f));
            bullet.GetComponent<Bullet>().damage = damage;
            bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.Euler(45f,0f,0f));
            bullet.GetComponent<Bullet>().damage = damage;
        }
    }
}
