using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private UnityEvent OnBegin, OnDone, OnDead;

    Rigidbody2D rb;
    Animator anim;
    Collider2D col;

    private int health, maxHealth;
    private HealthStats healthStats;
    private EnemyMelee enemy;
    private BossAbility boss;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<HealthBar>();
        col = GetComponent<Collider2D>();
        healthStats = new HealthStats(1000, 1000);
        health = healthStats.Health;
        maxHealth = healthStats.MaxHealth;
        maxHealth = health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dead();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttackZone"))
        {
            enemy = collision.GetComponentInParent<EnemyMelee>();
            StopAllCoroutines();
            OnBegin?.Invoke();
            anim.SetTrigger("Hit");
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(direction * 2, ForceMode2D.Impulse);
            IsDamaged(enemy.Damage);
            Debug.Log("Health = " + health + "Max health = " + maxHealth);
            StartCoroutine(Reset());
        }else if (collision.gameObject.CompareTag("Lightning"))
        {
            boss=FindObjectOfType<BossAbility>();
            StopAllCoroutines();
            OnBegin?.Invoke();
            anim.SetTrigger("Hit");
            /*Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(direction * 2, ForceMode2D.Impulse);*/
            IsDamaged(boss.Damage);
            Debug.Log("Health = " + health + "Max health = " + maxHealth);
            StartCoroutine(Reset());
        }else if (collision.gameObject.CompareTag("BossAttackZone"))
        {
            boss = FindObjectOfType<BossAbility>();
            StopAllCoroutines();
            OnBegin?.Invoke();
            anim.SetTrigger("Hit");
            Vector2 direction = (transform.position - collision.gameObject.transform.position).normalized;
            rb.AddForce(direction * 7, ForceMode2D.Impulse);
            IsDamaged(boss.Damage);
            Debug.Log(boss.Damage);
            Debug.Log("Health = " + health + "Max health = " + maxHealth);
            StartCoroutine(Reset());
        }
    }

    public void IsDamaged(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(.15f);
        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }

    void Dead()
    {
        if (health <= 0)
        {
            OnDead?.Invoke();
            anim.SetBool("Dead", true);
            GameManager.Instance.EndGame();
        }
    }
}
