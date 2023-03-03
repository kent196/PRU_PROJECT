using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject barrier;
    [SerializeField] private GameObject explosion;

    HealthStats healthStats;
    Animator anim;
    Collider2D col;

    int health, maxHealth;

    float vulnerableTime;
    float timer = 2f;
    bool isVulnerable = false;
    int shieldHitCount;
    int shieldLife = 10;

    int explosionCount = 5;
    float explosionRadius = .5f;

    PlayerAbilities playerability;
    // Start is called before the first frame update
    void Start()
    {
        BossHealth();
        vulnerableTime = timer;
        shieldHitCount = shieldLife;
        col = GetComponent<Collider2D>();
        healthStats = new HealthStats(health, maxHealth);
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
            return true;
        }
        else
        {
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
        StartCoroutine(Explosion());
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
        Destroy(gameObject);
    }

    void BossVulnerable()
    {
        isVulnerable = true;
    }
}
