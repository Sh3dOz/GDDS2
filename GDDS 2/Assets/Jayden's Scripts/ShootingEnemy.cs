using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public GameObject bulletPrefab;
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
    EnemySpawn spawnManager;
    public AudioSource UI;
    public AudioClip shootingSound;
    public AudioClip deathSound;
    public GameObject deathExplosion;
    public LevelManager manager;
    public GameObject hurtEffect;
    public AudioClip shotSound;


    void Start() {
        manager = FindObjectOfType<LevelManager>();
    }
    public void Shoot()
    {
        if (bulletCount > 0)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                bullet.transform.localScale = new Vector3(-1f, 1, 1f);
                bullet.GetComponent<Bullet>().speed = bulletSpeed;
                bullet.GetComponent<Bullet>().damage = damage;
                bullet.tag = "Enemy";
                bulletCount--;
                UI.PlayOneShot(shootingSound);
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

    public void TakeDamage(int damage, GameObject bullet)
    {
        manager = FindObjectOfType<LevelManager>();
        health -= damage;
        Instantiate(hurtEffect, transform.position, Quaternion.identity);
        UI.PlayOneShot(shotSound);
        if(health <= 0)
        {
            Die(bullet);
        }
    }

    public void Die(GameObject deathObject)
    {
        manager = FindObjectOfType<LevelManager>();
        spawnManager = FindObjectOfType<EnemySpawn>();
        foreach(GameObject i in spawnManager.enemiesSpawned)
        {
            if (i == this.gameObject)
            {
                spawnManager.enemiesSpawned.Remove(i);
                Instantiate(deathExplosion, i.transform.position, Quaternion.identity);
                Destroy(gameObject);
                manager.audioS.PlayOneShot(deathSound);
                break;
            }
        }
        if (!deathObject.GetComponent<MetoriteController>())
        {
            if (this.GetComponent<SpaceEnemy>())
            {
                PlayerPrefs.SetInt("AllKilledMetor", 1);
            }
        }
    }
}
