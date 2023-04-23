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
    public Transform shootPos;
    public float fireRate;
    float nextFire;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + offset, new Vector2(size,size), 0, -transform.right);
        if (bulletCount > 0) 
        { if (Time.time < nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
                bulletCount--;
            }
        }
        else
        {
            waitReload();
        }
    }

    IEnumerator waitReload()
    {
        yield return new WaitForSeconds(3f);
        bulletCount = maxBullet;
    }
}
