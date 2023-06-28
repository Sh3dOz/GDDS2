using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform spawnPosition;
    public Transform targetPosition;
    public GameObject laserBeam;
    public bool isEnemy;
    public enum currentMode {NotActive, Active, DoneFiring };
    public currentMode myMode;
    public PolygonCollider2D smallLaser;
    public PolygonCollider2D bigLaser;
    // Start is called before the first frame update
    void Start()
    {
        myMode = currentMode.NotActive;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemy)
        {
            if (myMode == currentMode.Active)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetPosition.position.x, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, targetPosition.position.y, 1f * Time.deltaTime));

                Vector2 pos = new Vector2(transform.position.x, transform.position.z),
                    targ = new Vector2(targetPosition.position.x, targetPosition.position.z),
                    dist = pos - targ;

                if (dist.sqrMagnitude < .001f)
                {
                    transform.parent = FindObjectOfType<Camera>().gameObject.transform;
                    //Charge Laser while moving backwards
                    laserBeam.SetActive(true);
                }
            }
            else if(myMode == currentMode.DoneFiring)
            {
                transform.position = new Vector2(Mathf.Lerp(transform.position.x, spawnPosition.position.x, 1f * Time.deltaTime), Mathf.Lerp(transform.position.y, spawnPosition.position.y, 1f * Time.deltaTime));

                Vector2 pos = new Vector2(transform.position.x, transform.position.z),
                    targ = new Vector2(spawnPosition.position.x, spawnPosition.position.z),
                    dist = pos - targ;

                if (dist.sqrMagnitude < .001f)
                {
                    Debug.Log("haro?");
                    //Destory
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void SmolLaser()
    {
        smallLaser.enabled = true;
        bigLaser.enabled = false;
    }

    public void ChunkyLaser()
    {
        smallLaser.enabled = false;
        bigLaser.enabled = true;
    }

    public void DiaableLaser()
    {
        laserBeam.SetActive(false);
        GetComponentInParent<LaserController>().myMode = currentMode.DoneFiring;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CameraCollider>() && myMode == currentMode.NotActive)
        {
            myMode = currentMode.Active;
            transform.position = spawnPosition.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<CameraCollider>() && myMode == currentMode.DoneFiring)
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
