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
    public float bulletSpeed;
    public Transform shootPos;
    public float fireRate;
    float nextFire;
    public int damage;
    public int health = 10;
    public int cost;
    public GameObject enemyPrefab;
    EnemySpawn manager;

    public void Shoot()
    {
        if (bulletCount > 0)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().speed = bulletSpeed;
                bullet.GetComponent<Bullet>().damage = damage;
                bulletCount--;
            }
        }
        else
        {
            if (isReloading) return;
            StartCoroutine(waitReload());
        }
    }
    IEnumerator waitReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(3f);
        bulletCount = maxBullet;
        isReloading = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        manager = FindObjectOfType<EnemySpawn>();
        foreach(GameObject i in manager.enemiesSpawned)
        {
            if (i == this.gameObject)
            {
                manager.enemiesSpawned.Remove(i);
                break;
            }
        }
    }
}
