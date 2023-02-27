using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    HealthStats healthStats;
    Animator anim;
    Collider2D col;
    [SerializeField] private HealthBar healthBar;


    int health, maxHealth;
    bool isVulnerable = false, isStaggering = false;
    float vulnerableCooldown, staggeringCooldown;
    int shieldHitCount = 10;
    PlayerAbilities playerability;

    // Start is called before the first frame update
    void Start()
    {
        BossHealth();
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
        Dead();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {

            anim.SetTrigger("Hit");
            healthStats.DamageUnit(playerability.Damage);
            healthBar.SetHealth(healthStats.Health);
            Debug.Log(playerability.Damage);
            Debug.Log(healthStats.Health);
        }
    }

    void Dead()
    {
        if (healthStats.Health <= 0)
        {
            col.isTrigger = true;
            anim.SetBool("Dead", true);
        }
    }

    void DestroyBoss()
    {
        Destroy(gameObject);
    }
}
