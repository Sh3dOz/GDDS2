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
        waveValue = currWave * 10;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
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
