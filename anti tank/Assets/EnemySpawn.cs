using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private static EnemySpawn _instance;
    public static EnemySpawn Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("EnemySpawn is NULL");
            }
            return _instance;
        }
    }
    public float difficulty = 1.005f;
    public float spawnLimit;
    public float spawnRate;
    public float currentSpawns;
    public bool canSpawn = true;
    bool isSpawning;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    bool quitLoop;

    private void Awake()
    {
        _instance = this;
        currentSpawns = 0;
    }

    private void Update()
    {
        if(currentSpawns < spawnLimit && canSpawn)
        {
            Debug.Log("starting loop");
            canSpawn = false;
            Invoke("SpawnEnemy", spawnRate);
            Debug.Log("Calling spawn");
        }
    }

    private void SpawnEnemy()
    {
        isSpawning = true;
        Debug.Log("Spawning");
        quitLoop = false;
        //while (!quitLoop)
        //{
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Collider[] intersecting = Physics.OverlapSphere(new Vector3(spawnPoints[randomSpawnPoint].position.x, spawnPoints[randomSpawnPoint].position.y, spawnPoints[randomSpawnPoint].position.z), 0.01f);
            if (intersecting.Length == 0)
            {
                //code to run if nothing is intersecting as the length is 0
                quitLoop = true;
                currentSpawns++;
                spawnRate = spawnRate / difficulty;
                Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnPoint].position, transform.rotation);
            }
            else
            {
                //code to run if something is intersecting it
            }
        //}
        isSpawning = false;
        canSpawn = true;
    }
}
