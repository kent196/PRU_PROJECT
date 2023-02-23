using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyType;
    [SerializeField] private List<GameObject> bossType;
    [SerializeField] private Transform[] spawnPosition;

    [SerializeField] private int enemyTotalIncrease = 10;
    [SerializeField] private const int waveTotal = 4;
    [SerializeField] private int currentWave = 1;

    List<GameObject> enemyToSpawn;
    List<GameObject> generateEnemy;
    Vector2[] spawnPos;

    private int enemyTotalPerWave;
    private int enemyTotalSpawned;
    // Start is called before the first frame update
    void Start()
    {
        enemyTotalPerWave = 10;
        enemyTotalSpawned = 0;
        spawnPos = new Vector2[]
        {
            spawnPosition[0].position,
            spawnPosition[1].position,
            spawnPosition[2].position,
        };
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void GetEnemyToSpawn()
    {
        GetEnemyTypeInWave(currentWave);
        generateEnemy = new List<GameObject>();
        for (int i = 0; i < enemyToSpawn.Count; i++)
        {
            generateEnemy.Add(enemyToSpawn[i]);
        }
        enemyToSpawn.Clear();
    }

    void GetEnemyTypeInWave(int currWave)
    {
        enemyToSpawn = new List<GameObject>();

        while (enemyToSpawn.Count < currWave)
        {
            if (currWave == waveTotal)
            {
                int randomIndex = UnityEngine.Random.Range(0, bossType.Count);
                GameObject randomBoss = bossType[randomIndex];

                if (!enemyToSpawn.Contains(randomBoss))
                {
                    enemyToSpawn.Add(randomBoss);
                }
            }
            else
            {
                int randomIndex = UnityEngine.Random.Range(0, enemyType.Count);
                GameObject randomEnemy = enemyType[randomIndex];

                if (!enemyToSpawn.Contains(randomEnemy))
                {
                    enemyToSpawn.Add(randomEnemy);
                }
            }

        }


    }

    IEnumerator SpawnEnemy()
    {
        int randomIndex = UnityEngine.Random.Range(0, spawnPos.Length);
        Vector3 randomPos = spawnPos[randomIndex];

        GetEnemyToSpawn();

        while (enemyTotalSpawned < enemyTotalPerWave)
        {
            for (int i = 0; i < generateEnemy.Count; i++)
            {
                for (int j = 0; j < enemyTotalPerWave / currentWave; j++)
                {
                    Instantiate(generateEnemy[i], randomPos, Quaternion.identity);
                    enemyTotalSpawned++;
                    yield return new WaitForSeconds(1);
                }
            }
        }
    }
}
