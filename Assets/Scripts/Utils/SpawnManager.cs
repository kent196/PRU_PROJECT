using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyWave1 = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyWave2 = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyWave3 = new List<GameObject>();
    [SerializeField] private List<GameObject> bossWave4 = new List<GameObject>();

    [SerializeField] private int enemyCount;
    [SerializeField] private int waveCount = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
