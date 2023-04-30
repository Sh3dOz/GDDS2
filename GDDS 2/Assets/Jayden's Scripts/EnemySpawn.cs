using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<GameObject> enemiesToSpawn;
    public List<GameObject> enemiesSpawned;
    public List<ShootingEnemy> waveToSpawn;
    public int currWave;
    public int waveValue;

    public Transform spawnLoaction;
    public int waveDuration;
    float waveTimer;
    float spawnInterval;
    float spawnTimer;

    public int index;
    public Transform topPos;
    public Transform midPos;
    public Transform botPos;
    // Start is called before the first frame update
    void Start()
    {
        GenerateWaves();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                GameObject enemies = Instantiate(enemiesToSpawn[0], spawnLoaction.position, Quaternion.identity);
                enemiesSpawned.Add(enemies);
                if(index == 0)
                {
                    enemies.GetComponent<SpaceEnemy>().targetPos = topPos;
                }
                else if(index == 1)
                {
                    enemies.GetComponent<SpaceEnemy>().targetPos = midPos;
                }
                else if(index == 2)
                {
                    enemies.GetComponent<SpaceEnemy>().targetPos = botPos;
                }
                index++;
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
        waveCheck();
    }

    void GenerateWaves()
    {
        waveValue = 3;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
        index = 0;
    }
    void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0)
        {
            int randEnemyID = Random.Range(0, waveToSpawn.Count);
            int randEnemyCost = waveToSpawn[randEnemyID].cost;

            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(waveToSpawn[randEnemyID].enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    void waveCheck()
    {
        if(enemiesSpawned.Count == 0)
        {
            currWave += 1;
            GenerateWaves();
        }
    }
}
