using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject explosion;

    [SerializeField] private HealthBar healthBar;
    HealthStats healthStats;
    Animator anim;
    Collider2D col;
    private GameObject bossHB;
    public GameObject
        victoryUI,
        imgShield;

    private SpawnManager sm;

    int health, maxHealth;

    float vulnerableTime;
    float timer = 6f;
    bool isVulnerable = false;
    int shieldHitCount;
    int shieldLife = 10;

    int explosionCount = 5;
    float explosionRadius = .5f;

    PlayerAbilities playerability;
    // Start is called before the first frame update
    void Start()
    {
        imgShield = GameObject.FindGameObjectWithTag("Shield");
        imgShield.SetActive(false);
        bossHB = GameObject.FindWithTag("Footer");
        BossHealth();
        healthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<HealthBar>();
        vulnerableTime = timer;
        shieldHitCount = shieldLife;
        col = GetComponent<Collider2D>();
        healthStats = new HealthStats(health, maxHealth);
        healthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<HealthBar>();
        anim = GetComponent<Animator>();
        playerability = FindObjectOfType<PlayerAbilities>();
    }




    private void BossHealth()
    {
        health = 2000;
        maxHealth = 2000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        VulnerableTimer();
        Dead();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (Vulnerable())
            {
                if (shieldHitCount == 0)
                {
                    anim.SetTrigger("BarrierBreak");
                }
                anim.SetTrigger("Hit");
                healthStats.DamageUnit(playerability.Damage);
                healthBar.SetHealth(healthStats.Health);
                Debug.Log("playerdamage:" + playerability.Damage);
                Debug.Log(healthStats.Health);
            }
            else
            {
                Instantiate(barrier, transform.position, Quaternion.identity);
                shieldHitCount--;
                Debug.Log(shieldHitCount);
            }
        }
    }



    bool Vulnerable()
    {

        if (shieldHitCount <= 0)
        {
            anim.SetBool("Vulnerable", isVulnerable);
            imgShield.SetActive(false);
            return true;
        }
        else
        {
            imgShield.SetActive(true);
            isVulnerable = false;
            return false;
        }
    }

    void VulnerableTimer()
    {
        if (Vulnerable())
        {
            vulnerableTime -= Time.deltaTime;
            if (vulnerableTime <= 0)
            {
                shieldHitCount = shieldLife;
                vulnerableTime = timer;
            }
        }
    }

    void Dead()
    {
        if (healthStats.Health <= 0)
        {
            col.isTrigger = true;
            anim.SetBool("Dead", true);
            Invoke("BossDestroy", 5f);
        }
    }

    void BossExplosion()
    {
        AudioManager.Instance.PauseMusic("Boss Round");
        AudioManager.Instance.PlaySFX("BossExplode", 0.2f);
        StartCoroutine(Explosion());
    }

    public void BossMelee()
    {
        AudioManager.Instance.PlaySFX("BossMelee", 0.2f);

    }

    IEnumerator Explosion()
    {
        for (int i = 0; i < explosionCount; i++)
        {
            yield return new WaitForSeconds(.2f);
            Vector2 pos = UnityEngine.Random.insideUnitCircle * explosionRadius;
            Instantiate(explosion, transform.position + new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
    }

    void BossDestroy()
    {
        AudioManager.Instance.PauseMusic("Boss Round");
        AudioManager.Instance.PlaySFX("Win", .5f);
        bossHB.SetActive(false);
        GameManager.Instance.Win();
        Destroy(gameObject);
    }

    void BossVulnerable()
    {
        isVulnerable = true;
    }
}
