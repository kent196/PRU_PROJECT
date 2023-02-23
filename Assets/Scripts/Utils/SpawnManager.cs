using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class SpawnManager : MonoBehaviour

{
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Animator animator;
    [SerializeField] private Text waveName;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private bool canAnimate = false;
    private bool canStart = false;

    private void Start()
    {
        waveName.text = waves[currentWaveNumber].waveName;
        animator.SetTrigger("WaveStart");
    }
    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = FindGameObjectsInLayer(LayerMask.NameToLayer("Enemy"));
        if (totalEnemies.Length == 0)
        {
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else
            {
                Debug.Log("GameFinish");
            }
        }
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        List<GameObject> validTransforms = new List<GameObject>();
        GameObject[] objs = GameObject.FindObjectsOfType<GameObject>() as GameObject[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].layer == layer)
                {
                    validTransforms.Add(objs[i]);
                }
            }
        }
        return validTransforms.ToArray();
    }
    void SpawnFirstWave()
    {
        canStart = true;
    }
    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()

    {
        if (canStart && canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}