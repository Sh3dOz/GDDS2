using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public Transform spawnPosition;
    public Transform targetPosition;
    public bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            transform.position = spawnPosition.position;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.position.x, Time.deltaTime), transform.position.y, transform.position.z);

            Vector2 pos = new Vector2(transform.position.x, transform.position.z),
                targ = new Vector2(targetPosition.position.x, targetPosition.position.z),
                dist = pos - targ;

            if (dist.sqrMagnitude < 0.0001f)
            {
                transform.parent = FindObjectOfType<Camera>().gameObject.transform;
                 //Charge Laser while moving backwards

            }
        }
    }
}
