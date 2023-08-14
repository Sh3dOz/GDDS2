using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpelWeapon : Weapon
{
    public override void Fire()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.tag = "Player";
            UI.PlayOneShot(shootingSound);
        }
    }
}
