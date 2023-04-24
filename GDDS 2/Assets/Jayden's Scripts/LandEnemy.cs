using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandEnemy : ShootingEnemy
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        bulletCount = maxBullet;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + offset, new Vector2(size, size), 0, -transform.right);
        if (hit.collider.gameObject.GetComponent<PlayerController>())
        {
            if (bulletCount > 0)
            {
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    GameObject bullet = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                    bullet.GetComponent<Bullet>().speed = -bullet.GetComponent<Bullet>().speed;
                    bulletCount--;
                }
            }
            else
            {
                if (isReloading) return;
                StartCoroutine(waitReload());
            }
        }
    }

    IEnumerator waitReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(3f);
        bulletCount = maxBullet;
        isReloading = false;
        Debug.Log("Reloaded");
    }
}
