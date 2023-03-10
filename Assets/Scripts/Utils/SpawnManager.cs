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
    private GameObject enemyHB;
    private GameManager gm;
    [SerializeField] private Wave[] waves;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform spawnPointFinalWave;
    [SerializeField] private Animator animator;
    [SerializeField] private Text waveName;
    private GameObject
        playerAbilities,
        playerMovement,
        playerBehavior,
        imgBoot,
        imgSword,
        imgHeart;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;
    private bool canAnimate = false;
    private bool canStart = false;

    private void Start()
    {
        imgBoot = GameObject.FindGameObjectWithTag("Boot");
        imgBoot.SetActive(false);
        imgSword = GameObject.FindGameObjectWithTag("Sword");
        imgSword.SetActive(false);
        imgHeart = GameObject.FindGameObjectWithTag("Heart");
        imgHeart.SetActive(false);
        playerAbilities = GameObject.FindGameObjectWithTag("Player");
        playerMovement = GameObject.FindGameObjectWithTag("Player");
        playerBehavior = GameObject.FindGameObjectWithTag("Player");
        enemyHB = GameObject.FindWithTag("Footer");
        enemyHB.SetActive(false);
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
                    Invoke("ChoosePowerUp", 1f);
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

    void ChoosePowerUp()
    {
        AudioManager.Instance.PlaySFX("PowUpSelection", .8f); GameManager.Instance.PowerUpSelect();
    }

    public void PowerUpSpdSelected()
    {
        AudioManager.Instance.PlaySFX("PowUpPicked", .8f);
        playerMovement.GetComponent<PlayerMovement>().Speed += 0.1f;
        playerBehavior.GetComponent<PlayerBehaviour>().Heal(150);
        imgBoot.SetActive(true);
        animator.SetBool("isPowerUpPicked", true);
        GameManager.Instance.PowerUpSelected();

    }
    public void PowerUpDmgSelected()
    {
        AudioManager.Instance.PlaySFX("PowUpPicked", .8f);
        imgSword.SetActive(true);
        playerAbilities.GetComponent<PlayerAbilities>().Damage += 20;
        playerBehavior.GetComponent<PlayerBehaviour>().Heal(150);
        animator.SetBool("isPowerUpPicked", true);
        GameManager.Instance.PowerUpSelected();

    }
    public void PowerUpHPSelected()
    {
        AudioManager.Instance.PlaySFX("PowUpPicked", .8f);
        imgHeart.SetActive(true);
        playerBehavior.GetComponent<PlayerBehaviour>().MaxHealthBuff(500);
        playerBehavior.GetComponent<PlayerBehaviour>().Heal(150);
        Debug.Log(playerAbilities.GetComponent<PlayerBehaviour>().MaxHealth);
        animator.SetBool("isPowerUpPicked", true);
        GameManager.Instance.PowerUpSelected();

    }
    void SpawnWave()

    {
        if (canStart && canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            if (currentWaveNumber + 1 != waves.Length)
            {
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
            else
            {
                AudioManager.Instance.PauseMusic("Theme");
                AudioManager.Instance.PlayMusic("Boss Round");
                enemyHB.SetActive(true);
                Instantiate(randomEnemy, spawnPointFinalWave.position, Quaternion.identity);
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
}