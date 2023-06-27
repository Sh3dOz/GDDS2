using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform spawnPosition;
    public Transform targetPosition;
    public bool isActivated;
    public GameObject laserBeam;
    public bool isEnemy;

    public PolygonCollider2D smallLaser;
    public PolygonCollider2D bigLaser;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            if (isActivated)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPosition.position.x, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosition.position.y, 1f * Time.deltaTime));

                Vector2 pos = new Vector2(transform.position.x, transform.position.z),
                    targ = new Vector2(targetPosition.position.x, targetPosition.position.z),
                    dist = pos - targ;

                if (dist.sqrMagnitude < .1f)
                {
                    transform.parent = FindObjectOfType<Camera>().gameObject.transform;
                    //Charge Laser while moving backwards
                    laserBeam.SetActive(true);
                }
            }
        }
    }

    public void SmolLaser()
    {
        smallLaser.enabled = true;
    }

    public void ChunkyLaser()
    {
        smallLaser.enabled = false;
        bigLaser.enabled = true;
    }

    public void DiaableLaser()
    {
        smallLaser.enabled = false;
        bigLaser.enabled = false;
    }
}
